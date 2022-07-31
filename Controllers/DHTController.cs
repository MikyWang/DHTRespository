using System.Net.Mime;
using DHTRespository.Data;
using DHTRespository.Models;
using Microsoft.AspNetCore.Mvc;
namespace DHTRespository.Controllers;

[ApiController]
[Route("[controller]")]
public class DHTController : ControllerBase
{

    private readonly ILogger<DHTController> _logger;
    private readonly DHTContext _dhtContext;

    public DHTController(ILogger<DHTController> logger, DHTContext dhtContext)
    {
        _logger = logger;
        _dhtContext = dhtContext;
    }

    [HttpGet("{hash}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InfoHash>> GetByHash(string hash)
    {
        var infohash = await _dhtContext.InfoHashes.FindAsync(hash);
        if (infohash is null) return BadRequest();
        return CreatedAtAction(nameof(GetByHash), infohash);
    }

    [HttpGet("GetInfoHashes")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IAsyncEnumerable<InfoHash>> GetAll()
    {
        return new ActionResult<IAsyncEnumerable<InfoHash>>(_dhtContext.InfoHashes.AsAsyncEnumerable());
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InfoHash>> AddInfoHash(InfoHash infoHash)
    {
        if (await _dhtContext.InfoHashes.FindAsync(infoHash.Hash) is not null)
        {
            return BadRequest();
        }

        await _dhtContext.AddAsync(infoHash);
        await _dhtContext.SaveChangesAsync();
        return CreatedAtAction(nameof(AddInfoHash), infoHash);
    }

}
