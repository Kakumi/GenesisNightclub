using AutoMapper;
using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Application.Member.Queries.Responses;
using GenesisNightclub.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Queries.GetMemberById
{
    public sealed class GetMemberByIdQueryHandler : IQueryHandler<GetMemberByIdQuery, MemberResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<Result<MemberResponse>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var memberDTO = await _memberRepository.GetMember(request.Id);
            var member = _mapper.Map<Domain.Models.Member> (memberDTO);

            if (member != null)
            {
                return new Result<MemberResponse>(true, new MemberResponse(member));
            }

            return new Result<MemberResponse>(false);
        }
    }
}
