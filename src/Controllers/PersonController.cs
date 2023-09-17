using cosmos_container.Data;
using cosmos_container.Inbound.Dtos;
using cosmos_container.OutBound.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IRepository<Person> _personRepo;

    public PersonController(IRepository<Person> personRepo)
    {
        _personRepo = personRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonDto personDto)
    {
        var presonDocument = new Person
        {
            Age = personDto.Age,
            Name = personDto.Name
        };

        var result = await _personRepo.CreateAsync(presonDocument);
        var returnDto = new CreatedPersonDto(result.Id.ToString(), result.Name, result.Age);

        return Ok(returnDto);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(string Id)
    {
        var result = await _personRepo.TryGetAsync(Id);
        if (result is null)
        {
            return NotFound();
        }
        var returnDto = new CreatedPersonDto(result.Id.ToString(), result.Name, result.Age);
        return Ok(returnDto);
    }
}