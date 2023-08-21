using AutoMapper;
using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Application.Member.Queries.Responses;
using GenesisNightclub.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Queries.GetMembers
{
    internal class GetMembersQueryHandler : IQueryHandler<GetMembersQuery, ICollection<MemberResponse>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetMembersQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<MemberResponse>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            var membersDTO = await _memberRepository.GetMembers();
            var members = membersDTO.Select(x => new MemberResponse(_mapper.Map<Domain.Models.Member>(x))).ToList();

            return new Result<ICollection<MemberResponse>>(true, (ICollection<MemberResponse>) members);
        }
    }
}
