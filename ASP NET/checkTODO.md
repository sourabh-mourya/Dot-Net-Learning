REST API

Prefer ActionResult<T> jab typed data ke saath error responses bhi chahiye. IActionResult bhi bilkul valid hai, especially jab multiple response types hon.

MVC web application

IActionResult common choice hai jab action View(), RedirectToAction() ya NotFound() return kar sakta ho. ViewResult tab use karo jab sirf view hi return karna ho.

Minimal API

IResult ya Results<T1, T2, ...> use kar sakte ho. Minimal APIs mein controller return types se thoda different pattern hota hai.

IActionResult Multiple MVC/API responses
ActionResult<T> Typed API data + error responses
IResult Minimal APIs mein Results.Ok(), Results.NotFound() etc.
ViewResult MVC HTML view
PartialViewResult Partial HTML view
JsonResult JSON response explicitly return karna
ContentResult Plain text ya specified content
RedirectResult Kisi URL par redirect karna
RedirectToActionResult Dusre controller action par redirect karna
FileResult File download ya file content return karna
EmptyResult Koi response body/result content nahi
