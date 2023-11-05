using Firebase.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GoogleAuthentication.Services;
namespace AUST_BUDDY_WEB.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public static string ApiKey = "AIzaSyDUqXI4YslT5jjdYCnS4EgXSvuzpg0xnyk";

        public static string Bucket = "asp-mvc-with-android.appspot.com";
        public ActionResult Index()
        {
            
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                // Verification.
                if (this.Request.IsAuthenticated)
                {
                    var ClientId = "1082440138378-3pfpkbpb0lbg64uq99rn9ll7mhdkj90f.apps.googleusercontent.com";
                    var url = "http://localhost:51065/Authentication/Login";
                    var response1 = GoogleAuth.GetAuthUrl(ClientId, url);
                    ViewBag.response1 = response1;//implementing sign in with Google
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info.
            return this.View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(AUST_BUDDY_WEB.Models.UserLogin model)
        {

            try
            {
                // Verification.
                if (ModelState.IsValid)
                {

                    var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                    var ab = await auth.SignInWithEmailAndPasswordAsync(model.Email, model.Password);

                    string token = ab.FirebaseToken;
                    var user = ab.User;

                    if (!string.IsNullOrEmpty(token))
                    {

                        // Set authentication cookie or session here
                        // For example: FormsAuthentication.SetAuthCookie(user.Email, false);
                        // You can also use your SignInUser method here
                        // SignInUser(user.Email, token, false);
                        this.SignInUser(user.Email, token, false);
                        Session["Email"] = user.Email.ToString();
                        return RedirectToAction("Index", "Authentication");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }


                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("INVALID_PASSWORD"))
                {
                    ModelState.AddModelError(string.Empty, "You have entered an invalid username or password");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        private void SignInUser(string email, string token, bool isPersistent)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Authentication, token)
            };

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Registration(AUST_BUDDY_WEB.Models.User model)
        {
            ViewBag.Message = "Your Registration page.";
            System.Diagnostics.Debug.WriteLine("Enter");
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var a = await auth.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password, model.FullName, true);
                System.Diagnostics.Debug.WriteLine("After create user");
                ModelState.AddModelError(string.Empty, "Please Verify your email then login Please.");
                if (ModelState.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("Successfull");
                    //return this.RedirectToAction("Login", "Authentication");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Failed");
                }
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS"))
                {
                    ModelState.AddModelError(string.Empty, "Try another username or email!");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View();
        }



        [AllowAnonymous]
        [HttpGet]
        public ActionResult Logout()
        {
            Session["Email"] = "";
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authentication");
        }
    }
}