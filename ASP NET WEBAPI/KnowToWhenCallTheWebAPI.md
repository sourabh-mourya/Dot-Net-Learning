Basic rule
Data lena (GET): response aata hai JSON string me, tumhe use C# object me Deserialize karna hai.
Data bhejna (POST/PUT): tumhare paas C# object hai, use JSON string me Serialize karke body me bhejna hai.
DELETE: body ki zaroorat nahi, sirf URL me id.

1. GET (list ya single)
   HttpResponseMessage response = await client.GetAsync(url);

if (response.IsSuccessStatusCode)
{
string result = await response.Content.ReadAsStringAsync(); // JSON string

    // List aa rahi hai to List<Student>, single record to Student
    var data = JsonConvert.DeserializeObject<List<Student>>(result);

}
Type wahi likhna jo API return karta hai. JSON [ {...}, {...} ] (square bracket) hai to List<Student>. JSON { ... } (curly bracket) hai to Student. Galat type diya to exception aayega.




2. POST / PUT (data bhejna)
string json = JsonConvert.SerializeObject(std);                              // 1. object -> JSON
var content = new StringContent(json, Encoding.UTF8, "application/json");    // 2. body banao
HttpResponseMessage response = await client.PostAsync(url, content);         // 3. bhejo (PUT ke liye PutAsync)

3. DELETE
HttpResponseMessage response = await client.DeleteAsync(url + id);



Har call ke baad ye check karo
csharp
if (response.IsSuccessStatusCode)   // 200-299
{
    // success
}
else
{
    // error body padho, ye debugging me sabse kaam aata hai
    string error = await response.Content.ReadAsStringAsync();
}

