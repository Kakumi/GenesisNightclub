using AutoMapper;
using AutoMapper.Execution;
using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Application.Member.Commands.DeleteMember;
using GenesisNightclub.Application.Member.Commands.RegisterMember;
using GenesisNightclub.Application.Member.Commands.UpdateMember;
using GenesisNightclub.Application.Member.Queries.GetMemberById;
using GenesisNightclub.Application.Member.Queries.GetMemberByNationalNumber;
using GenesisNightclub.Application.Member.Queries.GetMembers;
using GenesisNightclub.Domain.DTOs;
using GenesisNightclub.Domain.Exceptions;
using GenesisNightclub.Domain.Interfaces;
using GenesisNightclub.Domain.Models;
using GenesisNightclub.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public MemberService(ISender sender, IMapper mapper) 
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<Domain.Models.Member?> GetMember(int id)
        {
            var query = new GetMemberByIdQuery(id);
            var response = await _sender.Send(query);
            if (response.IsSuccess && response.Value != null)
            {
                return response.Value.Member;
            }

            return null;
        }

        public async Task<Domain.Models.Member?> GetMember(string nationalNumber)
        {
            var query = new GetMemberByNationalNumberQuery(nationalNumber);
            var response = await _sender.Send(query);
            if (response.IsSuccess && response.Value != null)
            {
                return response.Value.Member;
            }

            return null;
        }

        public async Task<List<Domain.Models.Member>> GetMembers()
        {
            var query = new GetMembersQuery();
            var response = await _sender.Send(query);
            if (response.IsSuccess && response.Value != null)
            {
                return response.Value.Select(x => x.Member).ToList();
            }

            return new List<Domain.Models.Member>();
        }

        public async Task<Result> RegisterMember(string contact,
            int cardNumber,
            string lastname,
            string firstname,
            DateTime birthdate,
            string nationalNumber,
            DateTime validFrom,
            DateTime validTo,
            int memberCardId)
        {
            var memberIDCard = await GetMember(nationalNumber);
            if (memberIDCard != null)
            {
                throw new ValidationException("An user is already registerd with the same identity card");
            }

            var command = new RegisterMemberCommand(contact,
            cardNumber,
            lastname,
            firstname,
            birthdate,
            nationalNumber,
            validFrom,
            validTo,
            memberCardId);

            return await _sender.Send(command);
        }

        public async Task<Result> UpdateMember(
            int id,
            string? contact,
            DateTime? endBlacklisted,
            string? firstname,
            string? lastname
        )
        {
            var member = await GetMember(id);
            if (member == null)
            {
                throw new NotFoundException($"The user with id {id} doesn't exists.");
            }

            var command = new UpdateMemberCommand(member, contact, endBlacklisted, firstname, lastname);
            var response = await _sender.Send(command);

            return response;
        }

        public async Task<Result> DeleteMember(int id)
        {
            var command = new DeleteMemberCommand(id);
            var response = await _sender.Send(command);

            return response;
        }
    }
}
