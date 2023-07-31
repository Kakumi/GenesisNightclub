using GenesisNightclub.Domain.DTOs;
using GenesisNightclub.Domain.Models;

namespace GenesisNightclub.Tests
{
    [TestClass]
    public class MemberTests
    {
        [TestMethod]
        public void IsValidContact()
        {
            var memberCard = new MemberCard(1);
            var identityCard = new IdentityCard(
                1,
                "Damien",
                "Brebion",
                new DateTime(1998, 10, 28),
                "998.10.28-000-28",
                new DateTime(1998, 10, 28),
                new DateTime(2100, 10, 28)
            );

            var member1 = new Member(identityCard, memberCard, "test@gmail.com");
            var member2 = new Member(identityCard, memberCard, "0495054375");
            var member3 = new Member(identityCard, memberCard, "+32495054375");
            var member4 = new Member(identityCard, memberCard, "test@gmail.c");
            var member5 = new Member(identityCard, memberCard, "test");

            Assert.IsTrue(member1.IsValidContact());
            Assert.IsTrue(member2.IsValidContact());
            Assert.IsTrue(member3.IsValidContact());
            Assert.IsFalse(member4.IsValidContact());
            Assert.IsFalse(member5.IsValidContact());
        }

        [TestMethod]
        public void IsAdult()
        {
            var memberCard = new MemberCard(1);
            var identityCard = new IdentityCard(
                1,
                "Damien",
                "Brebion",
                new DateTime(1998, 10, 28),
                "998.10.28-000-28",
                new DateTime(1998, 10, 28),
                new DateTime(2100, 10, 28)
            );
            var identityCard2 = new IdentityCard(
                1,
                "Damien",
                "Brebion",
                new DateTime(2010, 10, 28),
                "998.10.28-000-28",
                new DateTime(1998, 10, 28),
                new DateTime(2100, 10, 28)
            );

            var member1 = new Member(identityCard, memberCard, "test@gmail.com");
            var member2 = new Member(identityCard2, memberCard, "0495054375");

            Assert.IsTrue(member1.IdentityCard.IsAdult());
            Assert.IsFalse(member2.IdentityCard.IsAdult());
        }

        [TestMethod]
        public void IsValidNationalNumber()
        {
            var memberCard = new MemberCard(1);
            var identityCard = new IdentityCard(
                1,
                "Damien",
                "Brebion",
                new DateTime(1998, 10, 28),
                "998.10.28-000-28",
                new DateTime(1998, 10, 28),
                new DateTime(2100, 10, 28)
            );
            var identityCard2 = new IdentityCard(
                1,
                "Damien",
                "Brebion",
                new DateTime(2010, 10, 28),
                "9985.10.28-000-28",
                new DateTime(1998, 10, 28),
                new DateTime(2100, 10, 28)
            );

            var member1 = new Member(identityCard, memberCard, "test@gmail.com");
            var member2 = new Member(identityCard2, memberCard, "0495054375");

            Assert.IsTrue(member1.IdentityCard.IsValidNationNumber());
            Assert.IsFalse(member2.IdentityCard.IsValidNationNumber());
        }
    }
}
