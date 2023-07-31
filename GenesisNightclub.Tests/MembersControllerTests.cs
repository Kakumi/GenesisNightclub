using AutoMapper;
using GenesisNightclub.Domain.DTOs;
using GenesisNightclub.Domain.Interfaces;
using GenesisNightclub.Domain.Mappers;
using GenesisNightclub.Repository.Contexts;
using GenesisNightclub.Repository.Repositories;
using GenesisNightclub.Service.Services;
using GenesisNightclub.Web.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using GenesisNightclub.Domain.Models;

namespace GenesisNightclub.Tests
{
    [TestClass]
    public class MembersControllerTests
    {
        private MembersController _controller;
        private NightclubContext _context;

        [TestInitialize] 
        public void Initialize()
        {
            var logger = Mock.Of<ILogger<MembersController>>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MemberProfile())).CreateMapper();

            var options = new DbContextOptionsBuilder<NightclubContext>()
            .UseInMemoryDatabase(databaseName: "genesis_consult")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new NightclubContext(options))
            {
                context.MemberCards.Add(new MemberCardDTO { Id = 1 });
                context.IdentityCards.Add(new IdentityCardDTO
                {
                    Number = 1,
                    Lastname = "Damien",
                    Firstname = "Brebion",
                    Birthdate = new DateTime(1998, 10, 28),
                    NationalNumber = "998.10.28-000-28",
                    ValidFrom = new DateTime(1998, 10, 28),
                    ValidTo = new DateTime(2100, 10, 28),
                });
                context.Members.Add(new MemberDTO()
                {
                    MemberCardId = 1,
                    IdentityCardNumber = 1,
                    Contact = "test@test.com",
                    EndBlacklisted = null
                });
                context.SaveChanges();
            }
            _context = new NightclubContext(options);

            var memberRepository = new SqlMemberRepository(_context);
            var memberService = new MemberService(memberRepository, mapper);

            _controller = new MembersController(logger, memberService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task TestGetMember()
        {
            var response = await _controller.GetMember(1);

            Assert.AreEqual(((IStatusCodeActionResult)response).StatusCode, 200);
        }

        [TestMethod]
        public async Task TestGetMembers()
        {
            var response = await _controller.GetMembers();

            Assert.AreEqual(((IStatusCodeActionResult)response).StatusCode, 200);
        }

        [TestMethod]
        public async Task TestGetMemberNotFound()
        {
            var response = await _controller.GetMember(999);

            Assert.AreEqual(((IStatusCodeActionResult)response).StatusCode, 404);
        }

        [TestMethod]
        public async Task TestCreateMember()
        {
            var response = await _controller.Register(new Web.Forms.RegisterMemberForm()
            {
                Contact = "test@gmail.com",
                MemberCard = new Web.Forms.MemberCardForm()
                {
                    Id = 2
                },
                IdentityCard = new Web.Forms.IdentityCardForm()
                {
                    Number = 2,
                    Lastname = "Unit",
                    Firstname = "Test",
                    Birthdate = new DateTime(1998, 10, 28),
                    NationalNumber = "123.45.67-000-00",
                    ValidFrom = new DateTime(1998, 10, 28),
                    ValidTo = new DateTime(2100, 10, 28)
                }
            });

            Assert.IsTrue(response is CreatedAtActionResult);
            var createdResponse = (CreatedAtActionResult)response;
            Assert.AreEqual(createdResponse.StatusCode, 201);
            Assert.IsTrue(createdResponse.Value is Member);
            var member = (Member)createdResponse.Value;
            Assert.AreEqual(member.Contact, "test@gmail.com");
            Assert.AreEqual(member.IdentityCard.Lastname, "Unit");
            Assert.AreEqual(member.IdentityCard.Firstname, "Test");
        }

        [TestMethod]
        public async Task TestCreateMemberErrors()
        {
            var form = new Web.Forms.RegisterMemberForm()
            {
                Contact = "test@gmail.com",
                MemberCard = new Web.Forms.MemberCardForm()
                {
                    Id = 2
                },
                IdentityCard = new Web.Forms.IdentityCardForm()
                {
                    Number = 2,
                    Lastname = "Unit",
                    Firstname = "Test",
                    Birthdate = new DateTime(1998, 10, 28),
                    NationalNumber = "1235.45.67-000-00",
                    ValidFrom = new DateTime(1998, 10, 28),
                    ValidTo = new DateTime(2100, 10, 28)
                }
            };

            //Invalid national number
            var response = await _controller.Register(form);
            Assert.AreEqual(((IStatusCodeActionResult)response).StatusCode, 400);

            //Invalid ValidTo
            form.IdentityCard.NationalNumber = "123.45.67-000-00";
            form.IdentityCard.ValidTo = new DateTime(1600, 10, 28);
            Assert.AreEqual(((IStatusCodeActionResult)response).StatusCode, 400);

            //Invalid ValidTo (expired)
            form.IdentityCard.ValidTo = new DateTime(2022, 10, 28);
            Assert.AreEqual(((IStatusCodeActionResult)response).StatusCode, 400);

            //Invalid Identity card exist
            form.IdentityCard.ValidTo = new DateTime(2100, 10, 28);
            form.IdentityCard.Number = 1;
            Assert.AreEqual(((IStatusCodeActionResult)response).StatusCode, 400);
        }

        [TestMethod]
        public async Task TestUpdateMember()
        {
            var response = await _controller.Update(1, new Web.Forms.UpdateMemberForm()
            {
                Firstname = "Test 2",
                EndBlacklisted = new DateTime(2050, 10, 28)
            });

            Assert.IsTrue(response is NoContentResult);
            var createdResponse = (NoContentResult)response;
            Assert.AreEqual(createdResponse.StatusCode, 204);
        }
    }
}