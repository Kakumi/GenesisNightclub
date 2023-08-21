using GenesisNightclub.Application.Member.Commands.RegisterMember;
using GenesisNightclub.Domain.Exceptions;
using GenesisNightclub.Domain.Interfaces;
using GenesisNightclub.Domain.Models;
using GenesisNightclub.Domain.Shared;
using GenesisNightclub.Web.Forms;
using Microsoft.AspNetCore.Mvc;

namespace GenesisNightclub.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IMemberService _memberService;

        public MembersController(ILogger<MembersController> logger, IMemberService memberService)
        {
            _logger = logger;
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            try
            {
                _logger.LogInformation($"Fetching all members");

                var members = await _memberService.GetMembers();
                return Ok(members);
            }
            catch (ValidationException vex)
            {
                return BadRequest(vex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching member with id {id}");

                var member = await _memberService.GetMember(id);
                if (member == null)
                {
                    return NotFound();
                }

                return Ok(member);
            }
            catch (ValidationException vex)
            {
                return BadRequest(vex.Message);
            }
            catch (NotFoundException nfex)
            {
                return NotFound(nfex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(500);
            }
        }

        [HttpGet("blacklisted/{id}")]
        public async Task<IActionResult> IsMemberBlacklisted(int id)
        {
            try
            {
                var member = await _memberService.GetMember(id);
                if (member == null)
                {
                    return NotFound();
                }

                return Ok(member.IsBlacklisted());
            }
            catch (ValidationException vex)
            {
                return BadRequest(vex.Message);
            }
            catch (NotFoundException nfex)
            {
                return NotFound(nfex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(500);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterMemberForm registerForm)
        {
            try
            {
                if (registerForm == null)
                {
                    return BadRequest("Invalid body");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _memberService.RegisterMember(registerForm.Contact!,
                    registerForm.IdentityCard!.Number,
                    registerForm.IdentityCard.Lastname,
                    registerForm.IdentityCard.Firstname,
                    registerForm.IdentityCard.Birthdate!.Value,
                    registerForm.IdentityCard.NationalNumber,
                    registerForm.IdentityCard.ValidFrom!.Value,
                    registerForm.IdentityCard.ValidTo!.Value,
                    registerForm.MemberCard!.Id);

                if (result.IsSuccess)
                {
                    return Created(nameof(Register), null);
                }

                return BadRequest();
            }
            catch (ValidationException vex)
            {
                return BadRequest(vex.Message);
            }
            catch (NotFoundException nfex)
            {
                return NotFound(nfex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(500);
            }
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMemberForm updateMemberForm)
        {
            try
            {
                if (updateMemberForm == null)
                {
                    return BadRequest("Invalid body");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _memberService.UpdateMember(id,
                    updateMemberForm.Contact,
                    updateMemberForm.EndBlacklisted,
                    updateMemberForm.Firstname,
                    updateMemberForm.Lastname);

                if (response.IsSuccess)
                {
                    return NoContent();
                }

                return BadRequest();
            }
            catch (ValidationException vex)
            {
                return BadRequest(vex.Message);
            }
            catch (NotFoundException nfex)
            {
                return NotFound(nfex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(500);
            }
        }
    }
}