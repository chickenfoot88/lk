using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAppAuth.Context;
using WebAppAuth.Entities;

namespace WebAppAuth.Controllers
{
    public class ClaimKindTemplatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClaimKindTemplates
        public ActionResult Index()
        {
            var claimKindTemplate = db.ClaimKindTemplate.Include(c => c.ClaimKind);
            return View(claimKindTemplate.ToList());
        }

        // GET: ClaimKindTemplates/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimKindTemplate claimKindTemplate = db.ClaimKindTemplate.Find(id);
            if (claimKindTemplate == null)
            {
                return HttpNotFound();
            }
            return View(claimKindTemplate);
        }

        // GET: ClaimKindTemplates/Create
        public ActionResult Create()
        {
            ViewBag.ClaimKindId = new SelectList(db.DictClaimKind, "Id", "Name");
            return View();
        }

        // POST: ClaimKindTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClaimKindId,Template,CreationTime,ChangedTime")] ClaimKindTemplate claimKindTemplate)
        {
            if (ModelState.IsValid)
            {
                db.ClaimKindTemplate.Add(claimKindTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClaimKindId = new SelectList(db.DictClaimKind, "Id", "Name", claimKindTemplate.ClaimKindId);
            return View(claimKindTemplate);
        }

        // GET: ClaimKindTemplates/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimKindTemplate claimKindTemplate = db.ClaimKindTemplate.Find(id);
            if (claimKindTemplate == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClaimKindId = new SelectList(db.DictClaimKind, "Id", "Name", claimKindTemplate.ClaimKindId);
            return View(claimKindTemplate);
        }

        // POST: ClaimKindTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClaimKindId,Template,CreationTime,ChangedTime")] ClaimKindTemplate claimKindTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(claimKindTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClaimKindId = new SelectList(db.DictClaimKind, "Id", "Name", claimKindTemplate.ClaimKindId);
            return View(claimKindTemplate);
        }

        // GET: ClaimKindTemplates/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimKindTemplate claimKindTemplate = db.ClaimKindTemplate.Find(id);
            if (claimKindTemplate == null)
            {
                return HttpNotFound();
            }
            return View(claimKindTemplate);
        }

        // POST: ClaimKindTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ClaimKindTemplate claimKindTemplate = db.ClaimKindTemplate.Find(id);
            db.ClaimKindTemplate.Remove(claimKindTemplate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: ClaimKindTemplates/GetByClaimKind?claimKindId=5
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetByClaimKind(long? claimKindId)
        {
            if (claimKindId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ClaimKindTemplates = (await db.ClaimKindTemplate.Where(x => x.ClaimKindId == claimKindId.Value)
            .ToListAsync())
                    .OrderBy(x => x.Template)
                    .Select(x =>
                        new SelectListItem
                        {
                            Text = x.Template,
                            Value = x.Id.ToString()
                        });

            return PartialView();
        }
    }
}
