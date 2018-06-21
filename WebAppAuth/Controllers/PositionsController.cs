using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using WebAppAuth.Context;
using WebAppAuth.Entities;
using WebAppAuth.Models;

namespace WebAppAuth.Controllers
{
    [Authorize(Roles = AppConstants.RoleAdministrator)]
    public class PositionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Positions
        public async Task<ActionResult> Index()
        {
            return View(await db.Position.ToListAsync());
        }

        // GET: Positions/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var position = await db.Position.FindAsync(id);
            if (position == null)
            {
                return HttpNotFound();
            }

            var claimKinds = await db.PositionClaimKind
                .Include(x => x.DictClaimKind)
                .Where(x => x.PositionId == id)
                .Select(x => x.DictClaimKind)
                .ToListAsync();

            ViewData["ClaimKinds"] = 
                await db.DictClaimKind
                    .Where(x => x.IsVisible)
                    .Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        })
                    .OrderBy(x => x.Text)
                    .ToListAsync();

            var data = new PositionModel
            {
                Id = position.Id,
                Name = position.Name,
                CreationTime = position.CreationTime,
                ChangedTime = position.ChangedTime,
                ClaimKinds = claimKinds
            };
            return View(data);
        }

        // GET: Positions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CreationTime,ChangedTime")] Position position)
        {
            if (ModelState.IsValid)
            {
                db.Position.Add(position);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var position = await db.Position.FindAsync(id);
            if (position == null)
            {
                return HttpNotFound();
            }

            var claimKinds = db.PositionClaimKind
                .Include(x=>x.DictClaimKind)
                .Where(x => x.PositionId == id)
                .Select(x=>x.DictClaimKind)
                .ToList();

            ViewData["ClaimKinds"] =
                db.DictClaimKind
                    .Where(x => x.IsVisible)
                    .Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        })
                    .OrderBy(x => x.Text)
                    .ToList();

            var data = new PositionModel
            {
                Id = position.Id,
                Name = position.Name,
                CreationTime = position.CreationTime,
                ChangedTime = position.ChangedTime,
                ClaimKinds = claimKinds
            };
            return View(data);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,Name,CreationTime,ChangedTime")] PositionModel position, long? claimKinds)
        {
            if (ModelState.IsValid)
            {
                var dbEntity = await db.Position.FindAsync(position.Id);
                if (dbEntity == null) return HttpNotFound();

                PositionModel.ApplyToEntity(dbEntity, position);
                db.Entry(dbEntity).State = EntityState.Modified;

                //если передан тип заявки - добавляем его
                if (claimKinds.HasValue)
                {
                    if (!db.PositionClaimKind.Any(x => x.PositionId == position.Id && x.DictClaimKindId == claimKinds))
                    {
                        db.PositionClaimKind.Add(new PositionClaimKind
                        {
                            DictClaimKindId = claimKinds.Value,
                            PositionId = position.Id
                        });
                    }
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Edit", new {position.Id});
            }

            ViewData["ClaimKinds"] =
                db.DictClaimKind
                    .Where(x => x.IsVisible)
                    .Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        })
                    .OrderBy(x => x.Text)
                    .ToList();
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = await db.Position.FindAsync(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Position position = await db.Position.FindAsync(id);
            db.Position.Remove(position);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Positions/DeleteClaimKind/5
        public async Task<ActionResult> DeleteClaimKind(long positionId, long claimKindId)
        {
            var position = await db.PositionClaimKind
                .FirstOrDefaultAsync(x => x.DictClaimKindId == claimKindId && x.PositionId == positionId);
            if (position == null) return HttpNotFound();

            db.PositionClaimKind.Remove(position);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", new { Id = positionId });
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
