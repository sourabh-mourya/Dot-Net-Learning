using ImageUploading.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploading.Controllers
{
    public class ProductController : Controller
    {
        private readonly ImageUploadingContext imageUploadingContext; // DB se kaam karne ke liye
        IWebHostEnvironment env; // wwwroot ka path lene ke liye (image save karne me kaam aata hai)

        // Dependency Injection: ASP.NET Core ye dono objects khud de deta hai
        public ProductController(ImageUploadingContext imageUploadingContext, IWebHostEnvironment env)
        {
            this.imageUploadingContext = imageUploadingContext;
            this.env = env;
        }

        // DB se saare products nikal kar Index view ko bhejta hai
        public IActionResult Index()
        {
            return View(imageUploadingContext.Products.ToList());
        }

        // GET: sirf khaali form dikhata hai
        public IActionResult AddProduct()
        {
            return View();
        }

        // POST: form submit hone par ye chalta hai
        // (form me enctype="multipart/form-data" hona compulsory hai)
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel prod)
        {
            string fileName = "";

            // Check: user ne image select ki hai ya nahi
            if (prod.photo != null)
            {
                var ext = Path.GetExtension(prod.photo.FileName).ToLower(); // file ka extension (.png, .jpg)
                var size = prod.photo.Length;                     // file size bytes me

                // Validation 1: sirf png, jpg, jpeg allowed
                if (ext.Equals(".png") || ext.Equals(".jpg") || ext.Equals(".jpeg"))
                {
                    // Validation 2: 1 MB (1000000 bytes) se kam honi chahiye
                    if (size <= 1000000)
                    {
                        // wwwroot/images folder ka path
                        string folder = Path.Combine(env.WebRootPath, "images");

                        // Unique naam: GUID lagane se same naam ki images overwrite nahi hoti
                        fileName = Guid.NewGuid().ToString() + "_" + prod.photo.FileName;

                        // Folder + file name = final path jaha image save hogi
                        string filePath = Path.Combine(folder, fileName);

                        // Image ko server ke us path par copy/save karna
                        prod.photo.CopyTo(new FileStream(filePath, FileMode.Create));

                        // DB me sirf file ka naam store hota hai, poori image nahi
                        Product p = new Product()
                        {
                            Name = prod.Name,
                            Price = prod.Price,
                            ImagePath = fileName
                        };

                        imageUploadingContext.Products.Add(p); // DB me add karo
                        imageUploadingContext.SaveChanges();   // changes save karo

                        // Success message (sirf ek redirect tak rehta hai)
                        TempData["Success"] = "Product added...";
                        return RedirectToAction("Index"); // Index page par bhejo
                    }
                    else
                    {
                        TempData["Size_Error"] = "Image must be less than 1 mb.";
                    }
                }
                else
                {
                    TempData["Ext_Error"] = "Only PNG, JPG, JPEG extension images are allowed";
                }
            }

            // Error aane par ya photo na hone par form wapas dikhao
            return View();
        }


        // Delete: Index ke form se POST aayega
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = imageUploadingContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // Pehle server se image hatao
            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                var imagePath = Path.Combine(env.WebRootPath, "images", Path.GetFileName(product.ImagePath));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // Phir DB se row hatao
            imageUploadingContext.Products.Remove(product);
            imageUploadingContext.SaveChanges();

            TempData["Success"] = "Product deleted...";
            return RedirectToAction("Index");
        }

      
        // Edit GET: purana data form me bharke dikhata hai
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = imageUploadingContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var vm = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            ViewBag.ImagePath = product.ImagePath; // ViewModel me ImagePath nahi hai, isliye ViewBag se bhej rahe hain
            return View(vm);
        }

        // Edit POST: changes save karta hai
        [HttpPost]
        public IActionResult Edit(ProductViewModel prod)
        {
            // DB se purana product nikalo aur usi ko update karo
            var product = imageUploadingContext.Products.FirstOrDefault(p => p.Id == prod.Id);
            if (product == null)
            {
                return NotFound();
            }

            // Error aane par form wapas dikhane ke liye purani image ka path
            ViewBag.ImagePath = product.ImagePath;

            // [Required] wale validations check karo (Name, Price, photo)
            if (!ModelState.IsValid)
            {
                return View(prod);
            }

            var ext = Path.GetExtension(prod.photo!.FileName).ToLower();

            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg")
            {
                TempData["Ext_Error"] = "Only PNG, JPG, JPEG extension images are allowed";
                return View(prod);
            }

            if (prod.photo.Length > 1000000)
            {
                TempData["Size_Error"] = "Image must be less than 1 mb.";
                return View(prod);
            }

            // Nayi image save karo
            string folder = Path.Combine(env.WebRootPath, "images");
            string newFileName = Guid.NewGuid().ToString() + "_" + prod.photo.FileName;
            string newPath = Path.Combine(folder, newFileName);

            using (var stream = new FileStream(newPath, FileMode.Create))
            {
                prod.photo.CopyTo(stream);
            }

            // Purani image server se delete karo
            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                var oldPath = Path.Combine(folder, Path.GetFileName(product.ImagePath));
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }

            // DB me naya naam + Name + Price update
            product.ImagePath = newFileName;
            product.Name = prod.Name;
            product.Price = prod.Price;
            imageUploadingContext.SaveChanges();

            TempData["Success"] = "Product updated...";
            return RedirectToAction("Index");
        }

    }
}