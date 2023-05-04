using Microsoft.AspNetCore.Mvc;
using SimpApi.Base;
using SimpApi.Models;

namespace SimpApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private static List<Student> list;

    public StudentController()
    {
        list = GetList();
    }

    private List<Student> GetList()
    {
        List<Student> students = new();
        students.Add(new Student(1, "Deny", "deny@sellen.com", "5554445544"));
        students.Add(new Student(2, "Deny", "deny@sellen.com", "5554445544"));
        students.Add(new Student(3, "Deny", "deny@sellen.com", "5554445544"));
        students.Add(new Student(4, "Deny", "deny@sellen.com", "5554445544"));
        students.Add(new Student(5, "Deny", "deny@sellen.com", "5554445544"));
        return students;
    }

    [HttpGet]
    public CommonResponse<List<Student>> Get()
    {
        return new CommonResponse<List<Student>>(list);
    }

    [HttpGet("{id}")]
    public CommonResponse<Student> Get(int id)
    {
        var student = list.Where(x => x.Id == id).FirstOrDefault();
        if (student is null)
        {
            return new CommonResponse<Student>("Record not found.");
        }
        return new CommonResponse<Student>(student);
    }

    [HttpPost]
    public CommonResponse<List<Student>> Post([FromBody] Student request)
    {
        list.Add(request);
        return new CommonResponse<List<Student>>(list);
    }

    [HttpPut("{id}")]
    public CommonResponse<List<Student>> Put(int id, [FromBody] Student request)
    {
        request.Id = id;
        var student = list.Where(x => x.Id == request.Id).FirstOrDefault();
        if (student is null)
        {
            return new CommonResponse<List<Student>>("Record not found.");          
        }
        list.Remove(student);
        list.Add(request);
        return new CommonResponse<List<Student>>(list);

    }

    [HttpDelete("{id}")]
    public CommonResponse<List<Student>> Delete(int id)
    {
        var student = list.Where(x => x.Id == id).FirstOrDefault();
        if (student is null)
        {
            return new CommonResponse<List<Student>>("Record not found.");
        }
        list.Remove(student);
        return new CommonResponse<List<Student>>(list);
    }
}
