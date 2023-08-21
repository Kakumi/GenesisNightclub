using AutoMapper;
using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Domain.DTOs;
using GenesisNightclub.Domain.Exceptions;
using GenesisNightclub.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Commands.UpdateMember
{
    public sealed class UpdateMemberCommandHandler : ICommandHandler<UpdateMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            request.Member.Contact = request.Contact ?? request.Member.Contact;
            request.Member.EndBlacklisted = request.EndBlacklisted;
            request.Member.IdentityCard.Lastname = request.Lastname ?? request.Member.IdentityCard.Lastname;
            request.Member.IdentityCard.Firstname = request.Firstname ?? request.Member.IdentityCard.Firstname;

            var memberDTO = _mapper.Map<MemberDTO>(request.Member);
            await _memberRepository.UpdateMember(memberDTO);
            await _unitOfWork.SaveAsync();

            return Result.Success();
        }
    }
}
