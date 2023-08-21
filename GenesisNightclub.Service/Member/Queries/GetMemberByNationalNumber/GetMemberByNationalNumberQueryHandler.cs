using AutoMapper;
using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Application.Member.Queries.GetMemberById;
using GenesisNightclub.Application.Member.Queries.Responses;
using GenesisNightclub.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Queries.GetMemberByNationalNumber
{
    public sealed class GetMemberByNationalNumberQueryHandler : IQueryHandler<GetMemberByNationalNumberQuery, MemberResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetMemberByNationalNumberQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<Result<MemberResponse>> Handle(GetMemberByNationalNumberQuery request, CancellationToken cancellationToken)
        {
            var memberDTO = await _memberRepository.GetMember(request.NationalNumber);
            var member = _mapper.Map<Domain.Models.Member>(memberDTO);

            if (member != null)
            {
                return new Result<MemberResponse>(true, new MemberResponse(member));
            }

            return new Result<MemberResponse>(false);
        }
    }
}
