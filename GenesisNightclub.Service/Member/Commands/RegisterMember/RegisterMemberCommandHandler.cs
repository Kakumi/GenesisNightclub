using AutoMapper;
using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Domain.DTOs;
using GenesisNightclub.Domain.Models;
using GenesisNightclub.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Commands.RegisterMember
{
    internal class RegisterMemberCommandHandler : ICommandHandler<RegisterMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(RegisterMemberCommand request, CancellationToken cancellationToken)
        {
            var identityCard = IdentityCard.Create(
                    request.CardNumber,
                    request.Lastname,
                    request.Firstname,
                    request.Birthdate,
                    request.NationalNumber,
                    request.ValidFrom,
                    request.ValidTo);

            var memberCard = MemberCard.Create(request.MemberCardId);

            var member = Domain.Models.Member.Create(identityCard, memberCard, request.Contact!);

            var memberDTO = _mapper.Map<MemberDTO>(member);

            await _memberRepository.RegisterMember(memberDTO);
            await _unitOfWork.SaveAsync();

            return Result.Success();
        }
    }
}
