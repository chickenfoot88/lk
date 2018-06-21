using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OhDataManager.Entities.System;
using WebAppAuth.Context;

namespace WebAppAuth.Controllers
{
    public class OrganizationSubdivisionsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: OrganizationSubdivisions
        public async Task<ActionResult> Index()
        {
            var organizationSubdivision = _db.OrganizationSubdivision.Include(o => o.Organization);
            return View(await organizationSubdivision.ToListAsync());
        }

        // GET: OrganizationSubdivisions/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationSubdivision organizationSubdivision = await _db.OrganizationSubdivision.FindAsync(id);
            if (organizationSubdivision == null)
            {
                return HttpNotFound();
            }
            return View(organizationSubdivision);
        }

        // GET: OrganizationSubdivisions/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationId = new SelectList(_db.Organization, "Id", "FullName");
            return View();
        }

        // POST: OrganizationSubdivisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FullName,OrganizationSubdivisionExternalId,OrganizationId,CreationTime,ChangedTime")] OrganizationSubdivision organizationSubdivision)
        {
            if (ModelState.IsValid)
            {
                _db.OrganizationSubdivision.Add(organizationSubdivision);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrganizationId = new SelectList(_db.Organization, "Id", "FullName", organizationSubdivision.OrganizationId);
            return View(organizationSubdivision);
        }

        // GET: OrganizationSubdivisions/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationSubdivision organizationSubdivision = await _db.OrganizationSubdivision.FindAsync(id);
            if (organizationSubdivision == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationId = new SelectList(_db.Organization, "Id", "FullName", organizationSubdivision.OrganizationId);
            return View(organizationSubdivision);
        }

        // POST: OrganizationSubdivisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FullName,OrganizationSubdivisionExternalId,OrganizationId,CreationTime,ChangedTime")] OrganizationSubdivision organizationSubdivision)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(organizationSubdivision).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(_db.Organization, "Id", "FullName", organizationSubdivision.OrganizationId);
            return View(organizationSubdivision);
        }

        // GET: OrganizationSubdivisions/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationSubdivision organizationSubdivision = await _db.OrganizationSubdivision.FindAsync(id);
            if (organizationSubdivision == null)
            {
                return HttpNotFound();
            }
            return View(organizationSubdivision);
        }

        // POST: OrganizationSubdivisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            OrganizationSubdivision organizationSubdivision = await _db.OrganizationSubdivision.FindAsync(id);
            _db.OrganizationSubdivision.Remove(organizationSubdivision);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
