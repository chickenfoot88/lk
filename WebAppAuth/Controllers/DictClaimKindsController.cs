using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OhDataManager.Entities.System;
using WebAppAuth.Context;
using System.Linq;

namespace WebAppAuth.Controllers
{
    [Authorize(Roles = AppConstants.RoleAdministrator)]
    public class DictClaimKindsController : BaseController
    {
        // GET: DictClaimKinds
        public async Task<ActionResult> Index()
        {
            using (var db = new ApplicationDbContext())
            {
                var dictClaimKind = db.DictClaimKind.Include(d => d.DictClaimType);
                return View(await dictClaimKind.ToListAsync());
            }
        }

        // GET: DictClaimKinds/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                DictClaimKind dictClaimKind = await db.DictClaimKind.FindAsync(id);
                if (dictClaimKind == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Templates = await db.ClaimKindTemplate
                    .Where(x => x.ClaimKindId == id)
                    .Select(x => x.Template)
                    .ToListAsync();

                return View(dictClaimKind);
            }
        }

        // GET: DictClaimKinds/Create
        public ActionResult Create()
        {
            using (var db = new ApplicationDbContext())
            {
                ViewBag.DictClaimTypeId = new SelectList(db.DictClaimType, "Id", "Name");
            }
            return View();
        }

        // POST: DictClaimKinds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DictClaimTypeId,Name,Note,IsVisible,CreationTime,ChangedTime")] DictClaimKind dictClaimKind)
        {
            using (var db = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    db.DictClaimKind.Add(dictClaimKind);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.DictClaimTypeId = new SelectList(db.DictClaimType, "Id", "Name", dictClaimKind.DictClaimTypeId);
                return View(dictClaimKind);
            }
        }

        // GET: DictClaimKinds/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                DictClaimKind dictClaimKind = await db.DictClaimKind.FindAsync(id);
                if (dictClaimKind == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Templates = await db.ClaimKindTemplate
                    .Where(x => x.ClaimKindId == id)
                    .Select(x => x.Template)
                    .ToListAsync();

                return View(dictClaimKind);
            }
        }

        // POST: DictClaimKinds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DictClaimTypeId,Name,Note,IsVisible,CreationTime,ChangedTime")] DictClaimKind dictClaimKind)
        {
            using (var db = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(dictClaimKind).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.DictClaimTypeId = new SelectList(db.DictClaimType, "Id", "Name", dictClaimKind.DictClaimTypeId);
                return View(dictClaimKind);
            }
        }

        // GET: DictClaimKinds/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DictClaimKind dictClaimKind = await db.DictClaimKind.FindAsync(id);
                if (dictClaimKind == null)
                {
                    return HttpNotFound();
                }
                return View(dictClaimKind);
            }
        }

        // POST: DictClaimKinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            using (var db = new ApplicationDbContext())
            {
                DictClaimKind dictClaimKind = await db.DictClaimKind.FindAsync(id);
                db.DictClaimKind.Remove(dictClaimKind);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
}
