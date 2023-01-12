using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TitanAPIAdminConnection;

namespace Titan.Administration.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(TitanAdmin titanAdmin)
        {
            TitanAdmin = titanAdmin;
        }

        private TitanAdmin TitanAdmin { get; set; }

        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            var Users = await TitanAdmin.GetAllUsersAsync();

            return View(Users);
        }

        // GET: UsersController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            var UserDetails = await TitanAdmin.GetUserByIdAsync(id);

            ViewBag.UserEffectiveAccess = await TitanAdmin.GetUserEffectiveAccessAsync(id.ToString());

            ViewBag.UserRoles = await TitanAdmin.GetUserRolesAsync(id.ToString());

            return View(UserDetails);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUser user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var NewUser = await TitanAdmin.CreateUserAsync(user);

            return RedirectToAction(nameof(Index));
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var UserToEdit = await TitanAdmin.GetUserByIdAsync(id);

            ViewBag.UserEffectiveAccess = await TitanAdmin.GetUserEffectiveAccessAsync(id.ToString());

            ViewBag.UserRoles = await TitanAdmin.GetUserRolesAsync(id.ToString());

            return View(UserToEdit);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditUser user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await TitanAdmin.EditUserAsync(id.ToString(), user);

            return RedirectToAction(nameof(Index));
        }

        // POST: UsersController/Disable/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disable(int id)
        {
            await TitanAdmin.UserSetEnabledAsync(id.ToString(), false);

            return RedirectToAction(nameof(Index));
        }

        // POST: UsersController/Enable/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enable(int id)
        {
            await TitanAdmin.UserSetEnabledAsync(id.ToString(), true);

            return RedirectToAction(nameof(Index));
        }

        // GET: UsersController/Delete/5
        [HttpGet("[controller]/Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var UserToDelete = await TitanAdmin.GetUserByIdAsync(id);

            ViewBag.UserEffectiveAccess = await TitanAdmin.GetUserEffectiveAccessAsync(id.ToString());

            ViewBag.UserRoles = await TitanAdmin.GetUserRolesAsync(id.ToString());

            return View(UserToDelete);
        }

        // POST: UsersController/Delete/5
        [HttpPost("[controller]/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int id)
        {
            await TitanAdmin.DeleteUserAsync(id);

            return RedirectToAction(nameof(Index));
        }

        // GET: UsersController/ResetPassword/5
        [HttpGet("[controller]/ResetPassword/{id}")]
        public async Task<ActionResult> ResetPassword(int id)
        {
            var UserToReset = await TitanAdmin.GetUserByIdAsync(id);

            return View(UserToReset);
        }

        // POST: UsersController/ResetPassword/5
        [HttpPost("[controller]/ResetPassword/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPasswordPost(int id)
        {
            await TitanAdmin.AdminPasswordResetAsync(id);

            return RedirectToAction(nameof(Index));
        }

        // GET: UsersController/EditRoles/5
        [HttpGet("[controller]/EditRoles/{id}")]
        public async Task<ActionResult> EditRoles(int id)
        {
            var UserRoles = await TitanAdmin.GetUserRolesAsync(id.ToString());

            return View(UserRoles);
        }

        // POST: UsersController/EditRoles/5
        [HttpPost("[controller]/EditRoles/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRolesPost(int id, EditUserRoles editUserRoles)
        {
            await TitanAdmin.EditUserRolesAsync(id.ToString(), editUserRoles);

            return RedirectToAction(nameof(Index));
        }
    }
}
