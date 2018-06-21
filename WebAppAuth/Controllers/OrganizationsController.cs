using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OhDataManager.Entities.System;
using WebAppAuth.Context;

namespace WebAppAuth.Controllers
{
    [Authorize(Roles = AppConstants.RoleAdministrator)]
    public class OrganizationsController : BaseController
    {
        // GET: Organizations
        public async Task<ActionResult> Index()
        {
            using (var db = new ApplicationDbContext())
            {
                return View(await db.Organization.ToListAsync());
            }
        }

        // GET: Organizations/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new ApplicationDbContext())
            {
                var organization = await db.Organization.FindAsync(id);
                if (organization == null)
                {
                    return HttpNotFound();
                }
                return View(organization);
            }
        }

        // GET: Organizations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FullName,OrganizationExternalId,CreationTime,ChangedTime")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    organization.CreationTime = organization.ChangedTime = DateTime.Now;
                    db.Organization.Add(organization);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(organization);
        }

        // GET: Organizations/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                var organization = await db.Organization.FindAsync(id);
                if (organization == null)
                {
                    return HttpNotFound();
                }
                return View(organization);
            }
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FullName,OrganizationExternalId,ChangedTime")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    organization.ChangedTime = DateTime.Now;
                    db.Entry(organization).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                var organization = await db.Organization.FindAsync(id);
                if (organization == null)
                {
                    return HttpNotFound();
                }
                return View(organization);
            }
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            using (var db = new ApplicationDbContext())
            {
                var organization = await db.Organization.FindAsync(id);
                if (organization != null) db.Organization.Remove(organization);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
}
