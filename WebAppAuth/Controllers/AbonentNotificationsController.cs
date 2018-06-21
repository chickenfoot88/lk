using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OhDataManager.Entities.Main;
using WebAppAuth.Context;
using WebAppAuth.Models;

namespace WebAppAuth.Controllers
{
    [Authorize(Roles = AppConstants.RoleEmployee)]
    public class AbonentNotificationsController : BaseController
    {
        // GET: AbonentNotifications
        public async Task<ActionResult> Index()
        {
            using (var db = new ApplicationDbContext())
            {
                var data = (await db.Notification
                    .Select(AbonentNotificationModel.ProjectionExpression)
                    .ToListAsync());

                return View(data);
            }
        }

        // GET: AbonentNotifications/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                var model = (await db.Notification
                    .Select(AbonentNotificationModel.ProjectionExpression)
                    .FirstOrDefaultAsync(x => x.Id == id));
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View(model);
            }
        }

        // GET: AbonentNotifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AbonentNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "Id,Title,Content,HouseId,HouseFullAddress,CreationTime,ChangedTime")] AbonentNotificationModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    var entity = new Notification
                    {
                        CreationTime = DateTime.Now,
                        Organization = await GetCurrentOrganization(db)
                    };

                    AbonentNotificationModel.ApplyToEntity(entity, model);
                    db.Notification.Add(entity);

                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: AbonentNotifications/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                var model = (await db.Notification
                    .Select(AbonentNotificationModel.ProjectionExpression)
                    .FirstOrDefaultAsync(x => x.Id == id));
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View(model);
            }
        }

        // POST: AbonentNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,Title,Content")] AbonentNotificationModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    var entity = (await db.Notification
                        .FirstOrDefaultAsync(x => x.Id == model.Id));
                    if (entity == null)
                    {
                        return HttpNotFound();
                    }

                    AbonentNotificationModel.ApplyToEntity(entity, model);

                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // GET: AbonentNotifications/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                var model = (await db.Notification
                    .Select(AbonentNotificationModel.ProjectionExpression)
                    .FirstOrDefaultAsync(x => x.Id == id));

                if (model == null)
                {
                    return HttpNotFound();
                }
                return View(model);
            }
        }

        // POST: AbonentNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity = await db.Notification.FindAsync(id);
                if (entity == null)
                {
                    return HttpNotFound();
                }
                db.Notification.Remove(entity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
}
