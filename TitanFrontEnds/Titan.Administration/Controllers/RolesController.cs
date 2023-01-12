using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Titan.Administration.Functions;
using TitanAPIAdminConnection;

namespace Titan.Administration.Controllers
{
    public class RolesController : Controller
    {
        public RolesController(TitanAdmin titanAdmin)
        {
            TitanAdmin = titanAdmin;
        }

        private TitanAdmin TitanAdmin { get; set; }

        // GET: RolesController
        public async Task<ActionResult> Index()
        {
            var Roles = await TitanAdmin.GetAllRolesAsync();

            return View(Roles);
        }

        // GET: RolesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var RoleDetails = await TitanAdmin.GetRoleByIdAsync(id);

            return View(RoleDetails);

        }

        // GET: RolesController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(CreateRole role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }
            var CreateRole = await TitanAdmin.CreateRoleAsync(role);

            return RedirectToAction(nameof(Index));
        }

        // GET: RolesController/Edit/5
        //[HttpGet("Edit/{id}")]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var RoleToEdit = await TitanAdmin.GetRoleByIdAsync(id);

            return View(new EditRoleDTO()
            {
                FeatureAreas = RoleToEdit.FeatureAreas,
                Features = RoleToEdit.Features,
                Id = RoleToEdit.Id,
                RoleName = RoleToEdit.RoleName,
                Users = RoleToEdit.Users
            });
        }

        // POST: RolesController/Edit/5
        [HttpPost]//("{id}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] EditRoleDTO editRoleDTO)
        {
            var editRole = new EditRole();
            editRole._Features = new Dictionary<string, bool?>();

            foreach (var kvp in editRoleDTO._Features)
            {
                editRole._Features.Add(kvp.Key, kvp.Value);
            }

            var EditRole = await TitanAdmin.EditRoleAsync(id, editRole);

            return RedirectToAction(nameof(Index));
        }

        // GET: RolesController/Delete/5
        [HttpGet("[controller]/Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var RoleToDelete = await TitanAdmin.GetRoleByIdAsync(id);

            return View(RoleToDelete);
        }

        // POST: RolesController/Delete/5
        [HttpPost("[controller]/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int id)
        {
            await TitanAdmin.DeleteRoleAsync(id);

            return RedirectToAction(nameof(Index));
        }

        // GET: RolesController/EditUsers/5
        [HttpGet("[controller]/EditUsers/{id}")]
        public async Task<ActionResult> EditUsers(int id)
        {
            var UsersInRoleToEdit = await TitanAdmin.RoleUsersAsync(id);

            return View(UsersInRoleToEdit);
        }

        // POST: RolesController/EditUsers/5
        [HttpPost("[controller]/EditUsers/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUsersPost(int id, EditRoleUsers editRoleUsers)
        {
            await TitanAdmin.EditRoleUsersAsync(id, editRoleUsers);

            return RedirectToAction(nameof(Index), typeof(UsersController).ControllerName());
        }
    }
}
