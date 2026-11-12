1. IActionResult
Method ke aage kuch type nahi likhte, bas IActionResult
Status code bhej sakte ho (200, 404, 204...)
Data bhi bhej sakte ho (Ok(student)), par Swagger ko pata nahi chalta ki kya data aayega
Use karo: jab data nahi bhej rahe (Delete me NoContent())
csharp
public IActionResult GetStudent(int id)
{
    return Ok(student);
}


1. ActionResult<T>
Method ke aage type likhte ho: ActionResult<Student>
Status code bhi bhej sakte ho (404, 400...)
Data bhi bhej sakte ho (Ok(student))
<Student> likhne se Swagger ko pata chal jata hai ki success pe Student aayega
Use karo: jab data wapas bhej rahe ho (Get, Create, Update)
csharp
public ActionResult<Student> GetStudent(int id)
{
    return Ok(student);
}



3. Task<IActionResult>
Same as IActionResult, bas async version
Jab method me await use ho (database call)
Use karo: async Delete
csharp
public async Task<IActionResult> Delete(int id)
{
    await _db.SaveChangesAsync();
    return NoContent();
}



4. Task<ActionResult<T>>
Same as ActionResult<T>, bas async version
Jab method me await use ho aur data bhi return karna ho
Production me sabse zyada yahi use hota hai
Use karo: async Get, Create, Update
csharp
public async Task<ActionResult<Student>> GetStudent(int id)
{
    var student = await _db.Students.FindAsync(id);
    return Ok(student);
}
Yaad rakhne ka formula
Task<> = await use ho raha hai (database call)
<T> = data ka type batana hai (Swagger ke liye)