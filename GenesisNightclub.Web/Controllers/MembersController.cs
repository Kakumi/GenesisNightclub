using GenesisNightclub.Domain.Models;
using GenesisNightclub.Repository.Interfaces;
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
            } catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(500);
            }
        }

        //[HttpGet(Name = "IsBlacklisted")]
        //public async Task<IActionResult> IsUserBlacklisted()
        //{
        //    try
        //    {
        //        throw new NotImplementedException();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);

        //        return StatusCode(500);
        //    }
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterForm registerForm)
        {
            try
            {
                if (registerForm == null)
                {
                    return BadRequest("Invalid body");
                }

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(500);
            }
        }

        //[HttpPatch(Name = "Update")]
        //public async Task<IActionResult> Update()
        //{
        //    try
        //    {
        //        throw new NotImplementedException();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);

        //        return StatusCode(500);
        //    }
        //}

        //[HttpPatch(Name = "Blacklisted")]
        //public async Task<IActionResult> Blacklisted()
        //{
        //    try
        //    {
        //        throw new NotImplementedException();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);

        //        return StatusCode(500);
        //    }
        //}
    }
}