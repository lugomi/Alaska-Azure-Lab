using LabService.Exceptions;
using LabService.Models;
using MongoDB.Driver;

namespace LabService.Repositories;

public interface IPeopleRepository
{
    Task<ICollection<Person>> GetAllPeople();
    Task<Person> GetPerson(string name);
    Task<Person> AddPerson(Person person);
}

public class PeopleRepository : IPeopleRepository
{
    private readonly Lazy<IMongoCollection<Person>> _collection;

    public PeopleRepository(DatabaseConfiguration dbConfig)
    {
        _collection = new Lazy<IMongoCollection<Person>>(() => GetCollection(dbConfig));
    }

    public async Task<ICollection<Person>> GetAllPeople()
    {
        var result = await _collection.Value.FindAsync(new FilterDefinitionBuilder<Person>().Empty);

        return await result.ToListAsync();
    }

    public async Task<Person> GetPerson(string name)
    {
        var result = await _collection.Value.FindAsync(new FilterDefinitionBuilder<Person>().Where(p => p.Name == name),
            new FindOptions<Person, Person> {Limit = 1});

        var person = await result.FirstOrDefaultAsync();

        return person ?? throw new NotFoundException($"Person with name '{name}' not found.");
    }

    public async Task<Person> AddPerson(Person person)
    {
        if (string.IsNullOrWhiteSpace(person.Name))
        {
            throw new BadRequestException("Property 'name' cannot be empty.");
        }
        if (person.Age <= 0)
        {
            throw new BadRequestException("Property 'age' cannot be empty and must be a positive integer.");
        }
        if (string.IsNullOrWhiteSpace(person.FavoriteColor))
        {
            throw new BadRequestException("Property 'favoriteColor' cannot be empty.");
        }

        await _collection.Value.InsertOneAsync(person);

        return person;
    }

    private static IMongoCollection<Person> GetCollection(DatabaseConfiguration dbConfig)
    {
        var client = new MongoClient(dbConfig.ConnectionString);
        var database = client.GetDatabase("PeopleDatabase");
        return database.GetCollection<Person>("People");
    }
}