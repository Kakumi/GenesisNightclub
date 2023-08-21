using AutoMapper;
using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Commands.DeleteMember
{
    public sealed class DeleteMemberCommandHandler : ICommandHandler<DeleteMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetMember(request.Id);
            if (member != null)
            {
                await _memberRepository.DeleteMember(member);
                await _unitOfWork.SaveAsync();
            }

            return Result.Success();
        }
    }
}
