using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using WebAppAuth.Context;
using WebAppAuth.Entities;

namespace WebAppAuth.Controllers
{
    [Authorize(Roles = AppConstants.RoleAdministrator + ", "
                       + AppConstants.RoleEmployee)]
    public class EmployeesController : BaseController
    {
        // GET: Employees
        public async Task<ActionResult> Index()
        {
            using (var db = new ApplicationDbContext())
            {
                var employee = db.Employee
                    .Include(e => e.ApplicationUser)
                    .Include(e => e.Organization)
                    .Include(e => e.Position);
                return View(await employee.ToListAsync());
            }
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var employee = await db.Employee.FindAsync(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Organizations = db.EmployeeOrganization
                    .Where(x => x.EmployeeId == employee.Id);

                return View(employee);
            }
        }

        // GET: Employees/Create
        public async Task<ActionResult> Create()
        {
            using (var db = new ApplicationDbContext())
            {
                var employeeUserIds = db.Employee.Select(x => x.ApplicationUserId);
                var users = await db.Users
                    .Where(x => !employeeUserIds.Contains(x.Id))
                    .OrderByDescending(x => x.CreationTime)
                    .ToListAsync();
                ViewBag.ApplicationUserId = new SelectList(users, "Id", "Email");
                ViewBag.OrganizationId = new SelectList(await db.Organization.ToListAsync(), "Id", "FullName");
                ViewBag.PositionId = new SelectList(await db.Position.ToListAsync(), "Id", "Name");
                return View();
            }
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(
                Include =
                    "Id,ApplicationUserId,PositionId,Surname,Name,DisplayName,Patronymic,PhoneNumber,ImageData,ImageMimeType,OrganizationId,CreationTime,ChangedTime"
                )] Employee employee)
        {
            using (var db = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    db.Employee.Add(employee);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                var employeeUserIds = db.Employee.Select(x => x.ApplicationUserId);
                var users = await db.Users
                    .Where(x => !employeeUserIds.Contains(x.Id))
                    .OrderByDescending(x => x.CreationTime)
                    .ToListAsync();
                ViewBag.ApplicationUserId = new SelectList(users, "Id", "Email", employee.ApplicationUserId);
                ViewBag.OrganizationId = new SelectList(await db.Organization.ToListAsync(), "Id", "FullName",
                    employee.OrganizationId);
                ViewBag.PositionId = new SelectList(await db.Position.ToListAsync(), "Id", "Name");
                return View(employee);
            }
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                var employee = await db.Employee.FindAsync(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                var employeeUserIds = db.Employee.Select(x => x.ApplicationUserId);
                var users = await db.Users
                    .Where(x => !employeeUserIds.Contains(x.Id))
                    .OrderByDescending(x => x.CreationTime)
                    .ToListAsync();
                ViewBag.ApplicationUserId = new SelectList(users, "Id", "Email", employee.ApplicationUserId);
                ViewBag.OrganizationId = new SelectList(await db.Organization.ToListAsync(), "Id", "FullName",
                    employee.OrganizationId);
                ViewBag.PositionId = new SelectList(await db.Position.ToListAsync(), "Id", "Name", employee.PositionId);

                ViewBag.Organizations = db.EmployeeOrganization
                    .Where(x => x.EmployeeId == employee.Id);

                return View(employee);
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(
                Include =
                    "Id,ApplicationUserId,Surname,Name,DisplayName,Patronymic,PhoneNumber,ImageData,ImageMimeType,OrganizationId,CreationTime,ChangedTime,PositionId"
                )] Employee employee)
        {
            using (var db = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    employee.ChangedTime = DateTime.Now;
                    db.Entry(employee).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                var employeeUserIds = db.Employee.Select(x => x.ApplicationUserId);
                var users = await db.Users
                    .Where(x => !employeeUserIds.Contains(x.Id))
                    .OrderByDescending(x => x.CreationTime)
                    .ToListAsync();
                ViewBag.ApplicationUserId = new SelectList(users, "Id", "Email", employee.ApplicationUserId);
                ViewBag.OrganizationId = new SelectList(await db.Organization.ToListAsync(), "Id", "FullName",
                    employee.OrganizationId);
                ViewBag.PositionId = new SelectList(await db.Position.ToListAsync(), "Id", "Name", employee.PositionId);
                return View(employee);
            }
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                var employee = await db.Employee.FindAsync(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            using (var db = new ApplicationDbContext())
            {
                var employee = await db.Employee.FindAsync(id);
                if (employee != null) db.Employee.Remove(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Получение списка исполнителей по виду заявки
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetByClaimKind(long claimKindId)
        {
            using (var context = new ApplicationDbContext())
            {

                var positionIds = (await context.PositionClaimKind
                    .Where(x => x.DictClaimKindId == claimKindId)
                    .Select(x => x.PositionId)
                    .ToListAsync());

                ViewBag.EmployeeId = (await context.Employee
                    .Where(x => x.PositionId != null)
                    .Where(x => positionIds.Contains(x.PositionId.Value))
                    .ToListAsync())
                    .OrderBy(x => x.DisplayName)
                    .Select(x =>
                        new SelectListItem
                        {
                            Text = x.DisplayName,
                            Value = x.Id.ToString()
                        });

                return PartialView();
            }
        }
    }
}
