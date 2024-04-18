using apbd_5.Models;
using apbd_5.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace apbd_5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "name")
    {
        //Open connection
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        
        connection.Open();
        
        //Create command
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        List<string> possibleOrderBys = ["name", "idanimal", "description", "category", "area"];
        if (!possibleOrderBys.Contains(orderBy.ToLower()))
        {
            orderBy = "name";
        }
        command.CommandText = "SELECT * From Animal ORDER BY " + orderBy;
        
        //Execute command
        var reader = command.ExecuteReader();

        var animals = new List<Animal>();

        int idAnimal = reader.GetOrdinal("IdAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");
        int descriptionOrdinal = reader.GetOrdinal("Description");
        int categoryOrdinal = reader.GetOrdinal("Category");
        int areaOrdinal = reader.GetOrdinal("Area");

        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimal),
                Name = reader.GetString(nameOrdinal),
                Descriprion = reader.GetString(descriptionOrdinal),
                Category = reader.GetString(categoryOrdinal),
                Area = reader.GetString(areaOrdinal)
            });
            
        }
        
        return Ok(animals);

    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        //Open connection
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        
        connection.Open();
        
        //Create command
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = $"INSERT Into Animal VALUES (@animalName, @animalDescription, @animalCategory, @animalArea)";
        command.Parameters.AddWithValue("@animalName", animal.Name);
        command.Parameters.AddWithValue("@animalDescription", animal.Descriprion);
        command.Parameters.AddWithValue("@animalCategory", animal.Category);
        command.Parameters.AddWithValue("@animalArea", animal.Area);
        //Mykolanano group13.sh
        //Execute
        command.ExecuteNonQuery();
        
        return Created("", null);
    }

    [HttpPut("idAnimal")]
    public IActionResult EditAnimal(int idAnimal, AddAnimal animalForEdit)
    {
        //Open connection
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        
        connection.Open();
        //Create command
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM Animal WHERE IdAnimal = @idAnimal";
        command.Parameters.AddWithValue("@idAnimal", idAnimal);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            return NotFound("There is no animal with such id. Modification won't be done");
        }
        
        reader.Close();

        using SqlCommand command2 = new SqlCommand();
        command2.Connection = connection;
        command2.CommandText = "UPDATE Animal " +
                              "SET [Name] = @newName, Description = @newDesc, " +
                              "Category = @newCateg, Area = @newArea " +
                              "WHERE IdAnimal = @idAnimal ";
        command2.Parameters.AddWithValue("@newName", animalForEdit.Name);
        command2.Parameters.AddWithValue("@newDesc",animalForEdit.Descriprion);
        command2.Parameters.AddWithValue("@newCateg", animalForEdit.Category);
        command2.Parameters.AddWithValue("@newArea", animalForEdit.Area);
        command2.Parameters.AddWithValue("@idAnimal", idAnimal);
        command2.ExecuteNonQuery();

        return Ok();
    }

    [HttpDelete("idAnimal")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        //Open connection
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        
        connection.Open();
        //Create command
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM Animal WHERE IdAnimal = @idAnimal";
        command.Parameters.AddWithValue("@idAnimal", idAnimal);

        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            return NotFound("There is no animal with such id. Deletion won't be done");
        }
        
        reader.Close();
        
        using SqlCommand command2 = new SqlCommand();
        command2.Connection = connection;
        command2.CommandText = "DELETE FROM Animal " +
                               "WHERE IdAnimal = @idAnimal ";
        command2.Parameters.AddWithValue("@idAnimal", idAnimal);
        command2.ExecuteNonQuery();

        return Ok();

    }
}