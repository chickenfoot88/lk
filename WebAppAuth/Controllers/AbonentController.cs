using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using OhDataManager.Entities.Main;
using OhDataManager.Enums;
using WebAppAuth.Entities;
using WebAppAuth.Models;
using WebAppAuth.Services.Data;
using WebAppAuth.Services.FileManager;

namespace WebAppAuth.Controllers
{
    using System;
    using System.Net;
    using System.Text;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web;
    using Context;
    using DependencyResolver;
    using Microsoft.AspNet.Identity;
    using OhDataManager;

    /// <summary>
    /// Личный кабинет абонента
    /// </summary>
    [Authorize(
        Roles = AppConstants.RoleAbonent + ", " + AppConstants.RoleDirector + ", " + AppConstants.RoleAdministrator)]
    public class AbonentController : BaseController
    {
        //пароль из ЛК
        readonly string _paymentProviderPassword = AppConstants.PaymentProviderPassword;
        //логин из ЛК
        readonly string _paymentProviderLogin = AppConstants.PaymentProviderLogin;
        
        private bool CheckSignature(string Order_ID, string Status, string Signature)
        {
            return Signature ==
                   Order_ID.GetMd5Hash() + "&" +
                   Status.GetMd5Hash() + "&" +
                   _paymentProviderPassword.GetMd5Hash().GetMd5Hash().ToUpper();
        }

        /// <summary>
        /// Главная страница личного кабинета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var abonent = await GetCurrentAbonent(context);
                if (abonent == null) return RedirectToAction("AddPersonalAccount");

                var data = await context.Notification
                    .Select(AbonentNotificationModel.ProjectionExpression)
                    .OrderByDescending(x=>x.CreationTime)
                    .ToListAsync();

                return View(data);
            }
        }

        /// <summary>
        /// Профиль абонента
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AbonentProfile()
        {
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();

                var accounts = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!accounts.Any()) return RedirectToAction("AddPersonalAccount");
                if (accounts.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                ViewBag.AbonentEmail = context.Users.Find(userIdentityid).Email;

                return View(accounts.Single());
            }
        }

        /// <summary>
        /// Прикрепление лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddPersonalAccount()
        {
            return View();
        }

        /// <summary>
        /// Прикрепление лицевого счета
        /// </summary>
        /// <param name="inputArgument"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddPersonalAccount(PersonalAccount inputArgument)
        {
            //получаем введенные поля с формы
            //платежный код
            long paymentCode;
            long.TryParse(Request.Params["PaymentCode"], out paymentCode);
            if (paymentCode == default(long))
            {
                ViewBag.Result = "Неверный формат платежного кода";
                return View();
            }

            //ФИО
            var apartmentOwnerFullName = Request.Params["ApartmentOwnerFullName"];
            //номер квартиры
            var apartmentNumber = Request.Params["ApartmentNumber"];

            var personalAccountResult = await OhConfigurator.Container.Resolve<IAbonentService>()
                .GetPersonalAccount(paymentCode, apartmentOwnerFullName, apartmentNumber);

            if (!personalAccountResult.Success)
            {
                ViewBag.Result = "Лицевой счет не найден!";
                return View();
            }

            var userIdentityid = User.Identity.GetUserId<int>();
            using (var context = new ApplicationDbContext())
            {
                var abonent = new Abonent
                {
                    OrganizationId = personalAccountResult.Content.OrganizationId,
                    ApartmentFullAddress = personalAccountResult.Content.ApartmentFullAddress,
                    ApplicationUserId = userIdentityid,
                    PaymentCode = personalAccountResult.Content.PaymentCode,
                    PersonalAccountId = personalAccountResult.Content.Id,
                    CreationTime = DateTime.Now,
                    ChangedTime = DateTime.Now
                };
                context.Abonent.Add(abonent);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Общие сведения лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult PersonalAccountInfo()
        {
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();
                
                var accounts = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!accounts.Any()) return RedirectToAction("AddPersonalAccount");
                if (accounts.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                var account = accounts.Single();

                //характеристики лицевого счета
                var personalAccountInfo = DependencyProvider
                    .Resolve<DataManagersFactory>()
                    .GetPersonalAccountDataManager().GetPersonalAccountInfo(account.PersonalAccountId);
                
                return View(personalAccountInfo);
            }
        }

        /// <summary>
        /// Редактирование контактной информации абонента
        /// </summary>
        /// <param name="abonent"></param>
        /// <param name="loadImage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Abonent abonent, HttpPostedFileBase loadImage)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    if (loadImage != null)
                    {
                        abonent.ImageMimeType = loadImage.ContentType;
                        abonent.ImageData = new byte[loadImage.ContentLength];
                        loadImage.InputStream.Read(abonent.ImageData, 0, loadImage.ContentLength);
                    }

                    if (abonent.Id == 0)
                    {
                        context.Abonent.Add(abonent);
                    }
                    else
                    {
                        var dbEntry = context.Abonent.FindAsync(abonent.Id).Result;
                        if (dbEntry != null)
                        {
                            dbEntry.Id = abonent.Id;
                            dbEntry.Surname = abonent.Surname;
                            dbEntry.Name = abonent.Name;
                            dbEntry.Patronymic = abonent.Patronymic;
                            dbEntry.MobilePhoneNumber = abonent.MobilePhoneNumber;
                            dbEntry.HousePhoneNumber = abonent.HousePhoneNumber;
                            dbEntry.EmailNotificationAllowed = abonent.EmailNotificationAllowed;
                            dbEntry.SmsNotificationAllowed = abonent.SmsNotificationAllowed;
                            if (loadImage != null)
                            {
                                dbEntry.ImageData = abonent.ImageData;
                                dbEntry.ImageMimeType = abonent.ImageMimeType;
                            }
                        }
                    }
                    context.SaveChanges();

                    TempData["message"] = $"{abonent.Surname} {abonent.Name} {abonent.Patronymic} сохранен";
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        /// <summary>
        /// Получение аватарки абонента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileContentResult GetImage(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var abonent = context.Abonent.Find(id);
                return abonent != null ? File(abonent.ImageData, abonent.ImageMimeType) : null;
            }
        }

        /// <summary>
        /// Удаление аватарки абонента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult DelFile(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var abonent = context.Abonent.Find(id);
                if(abonent==null) throw new Exception($"Абонент с Id '{id}' не найден");

                abonent.ImageData = null;
                abonent.ImageMimeType = null;
                context.SaveChanges();

                TempData["message"] = $"{abonent.Surname} {abonent.Name} {abonent.Patronymic} сохранен";
                return RedirectToAction("Index");
            }
        }
        
        /// <summary>
        /// Список начислений лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Accruals(FormCollection formValue)
        {
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();

                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return RedirectToAction("AddPersonalAccount");
                if (abonents.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                var abonent = abonents.Single();

                var calculationDate = Convert.ToDateTime(formValue["CalculationDateSelectList"]);

                var calculationMonthList = context.CalculationMonth
                    .Where(x => x.Organization.Id == abonent.OrganizationId)
                    .OrderByDescending(g => g.CalculationDate).ToList();

                ViewData["CalculationDateSelectList"] =
                    (from month in calculationMonthList
                        select
                            new SelectListItem
                            {
                                Text = month.CalculationDate.ToString("y"),
                                Value = month.CalculationDate.ToString(CultureInfo.CurrentCulture),
                                Selected = (month.CalculationDate == calculationDate)
                            })
                        .ToList();

                var currentCalculationMonth = calculationMonthList.First(x => x.IsCurrent);

                var calculationMonth = calculationDate == default(DateTime)
                    ? currentCalculationMonth.CalculationDate
                    : calculationDate;

                var data = (await OhConfigurator.Container.Resolve<IAbonentService>()
                    .GetAccruals(abonent.PersonalAccountId, calculationMonth))
                    .Content.OrderBy(x=>x.IndexNumber);

                return View(data);
            }
        }
        
        /// <summary>
        /// Оплата по лицевому счету
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Pay()
        {
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();


                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return RedirectToAction("AddPersonalAccount");
                if (abonents.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                var abonent = abonents.Single();

                //характеристики лицевого счета
                var personalAccountInfo = DependencyProvider
                    .Resolve<DataManagersFactory>()
                    .GetPersonalAccountDataManager().GetPersonalAccountInfo(abonent.PersonalAccountId);

                var calculationDate =
                    context.CalculationMonth.Single(x => x.OrganizationId == abonent.OrganizationId && x.IsCurrent)
                        .CalculationDate;

                //начисления
                var accruals = (await DependencyProvider
                    .Resolve<DataManagersFactory>()
                    .GetPersonalAccountDataManager()
                    .GetPersonalAccountAccruals(abonent.PersonalAccountId, calculationDate))
                    .ToList();
                
                //начислено
                ViewBag.Accrued = accruals.Sum(x => x.AccruedForPayment ?? 0).ToString("N2");
                //Фамилия
                ViewBag.Fam = abonent.Surname;
                //Имя
                ViewBag.Ima = abonent.Name;
                //Отчество
                ViewBag.Otch = abonent.Patronymic;
                //Email
                ViewBag.Email = context.Users.Find(abonent.ApplicationUserId).Email;

               

                return View(personalAccountInfo);
            }
        }

        /// <summary>
        /// Оплата по лицевому счету (формирование запроса)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public string Pay(Abonent abonent)
        {
            var uniPay = new UniPay();

            //получаем введенные поля с формы
            //платежный код
            uniPay.CustomerIdp = Request.Params["Customer_IDP"];
            //Фамилия
            uniPay.LastName = Request.Params["LastName"];
            //Имя
            uniPay.FirstName = Request.Params["FirstName"];
            //Отчество
            uniPay.MiddleName = Request.Params["MiddleName"];
            //Инн
            uniPay.Inn = Request.Params["Inn"];
            //Адрес
            uniPay.Address = Request.Params["Address"];
            //Email
            uniPay.Email = Request.Params["Email"];
            //Комментарий
            uniPay.Comment = "Счёт-фактура на оплату жилищно-коммунальных услуг. ЛС №" + uniPay.CustomerIdp + ".";
            //Сумма платежа
            uniPay.SubtotalP = Convert.ToDecimal(Request.Params["Subtotal_P"]).ToString("f2").Trim();

            //Номер заказа
            //i.Order_IDP = DependencyProvider
            //    .Resolve<DataManagersFactory>()
            //    .GetPersonalAccountDataManager()
            //    .GetSeqOrderId();
            
            using (var context = new ApplicationDbContext())
            {
                uniPay.OrderIdp = context.Database.SqlQuery<long>("SELECT nextval('seq_order_id')").Single().ToString();
                
                //Адреса возврата после успешной и неуспешной оплат покупателям
                //TODO: параметризировать
                
                uniPay.UrlReturnOk = "http://81.22.206.222/test/otk/Abonent/PayResult";
                uniPay.UrlReturnNo = "http://81.22.206.222/test/otk/Abonent/Pay";

                //Идентификатор точки продажи в системе Uniteller 
                uniPay.ShopIdp = AppConstants.SalePointId;
                //время жизни формы
                uniPay.LifeTime = 3600;

                //ЭЦП
                uniPay.Signature = (
                    uniPay.ShopIdp.GetMd5Hash() + "&" +
                    uniPay.OrderIdp.GetMd5Hash() + "&" +
                    uniPay.SubtotalP.GetMd5Hash() + "&" +
                    "".GetMd5Hash() + "&" +
                    "".GetMd5Hash() + "&" +
                    uniPay.LifeTime.ToString().GetMd5Hash() + "&" +
                    uniPay.CustomerIdp.GetMd5Hash() + "&" +
                    "".GetMd5Hash() + "&" +
                    "".GetMd5Hash() + "&" +
                    "".GetMd5Hash() + "&" +
                    _paymentProviderPassword.GetMd5Hash()
                    ).GetMd5Hash();

                uniPay.Signature = uniPay.Signature.ToUpper();

                //DependencyProvider
                // .Resolve<DataManagersFactory>()
                // .GetPersonalAccountDataManager()
                // .SavePayResultPre(uniPay);

                context.UniPay.Add(uniPay);
                context.SaveChangesAsync();

            }
            var postedData = "{\"Shop_IDP\":\"" + uniPay.ShopIdp + "\",\"Order_IDP\":\"" + uniPay.OrderIdp +
                             "\",\"Subtotal_P\":\"" + uniPay.SubtotalP + "\"" +
                             ",\"Lifetime\":\"" + uniPay.LifeTime + "\",\"Customer_IDP\":\"" +
                             uniPay.CustomerIdp + "\"" +
                             ",\"Language\":\"ru\",\"Signature\":\"" + uniPay.Signature + "\",\"URL_RETURN_OK\":\"" +
                             uniPay.UrlReturnOk + "\",\"URL_RETURN_NO\":\"" + uniPay.UrlReturnNo + "\"" +
                             ",\"Comment\":\"" + uniPay.Comment + "\",\"Email\":\"" + uniPay.Email + "\"}";

            return postedData;
        }

        /// <summary>
        /// Результат оплаты по лицевому счету
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult PayResult(String Order_ID)
        {
            //URL
            const string postUrl = @"https://wpay.uniteller.ru/results/";
            //Идентификатор точки продажи в системе Uniteller 
            string Shop_ID = AppConstants.SalePointId;
            
            //Format =1 - получить данные в виде строки с разделителем ";", можно получать данные и 
            //в других форматах (см. Т ехнический порядок ), например, XML ,
            //тогда обработка полученного ответа изменится 
            
            var postedData =
                $"Shop_ID={Shop_ID}&Login={_paymentProviderLogin}&Password={_paymentProviderPassword}&Format=1" +
                $"&ShopOrderNumber={Order_ID}&S_FIELDS=Status;ApprovalCode;BillNumber";

            var response = PostMethod(postedData, postUrl);

            if (response != null)
            {
                var strreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                var responseToString = strreader.ReadToEnd();
                string[] result = responseToString.Split(';');
                if (result.Length == 3)
                {
                    DependencyProvider
                     .Resolve<DataManagersFactory>()
                     .GetPersonalAccountDataManager()
                     .UpdatePayResultPre(Order_ID, result);
                    return RedirectToAction("Pay");
                }
            }

            return RedirectToAction("Pay");
        }

        /// <summary>
        /// Отправка данных
        /// </summary>
        /// <returns></returns>
        private HttpWebResponse PostMethod(string postedData, string postUrl)
        {
            var request = (HttpWebRequest)WebRequest.Create(postUrl);
            request.Method = "POST";
            //заполняем заголовки
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "application/x-www-form-urlencoded";

            var bytes = new UTF8Encoding().GetBytes(postedData);
            request.ContentLength = bytes.Length;

            using (var newStream = request.GetRequestStream())
            {
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();
            }
            return (HttpWebResponse)request.GetResponse();
        }
        
        /// <summary>
        /// Приборы учета лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Counters(FormCollection formValue)
        {
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();

                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return RedirectToAction("AddPersonalAccount");
                if (abonents.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                var abonent = abonents.Single();

                var calculationDate = Convert.ToDateTime(formValue["CalculationDateSelectList"]);

                var calculationMonthList = context.CalculationMonth
                    .Where(x => x.Organization.Id == abonent.OrganizationId)
                    .OrderByDescending(g => g.CalculationDate).ToList();

                ViewData["CalculationDateSelectList"] =
                    (from month in calculationMonthList
                     select
                         new SelectListItem
                         {
                             Text = month.CalculationDate.ToString("y"),
                             Value = month.CalculationDate.ToString(CultureInfo.CurrentCulture),
                             Selected = (month.CalculationDate == calculationDate)
                         })
                        .ToList();
                
                var currentCalculationMonth = calculationMonthList.First(x => x.IsCurrent);

                var calculationMonth = calculationDate == default(DateTime)
                    ? currentCalculationMonth.CalculationDate
                    : calculationDate;
                
                var data = (await OhConfigurator.Container.Resolve<IAbonentService>()
                    .GetMeterReadings(abonent.PersonalAccountId, calculationMonth, calculationMonth)).Content;
                
                //добавляем уведомление, если есть ПУ с заканчивающейся датой поверки
                ViewBag.NotificationsList = (from counter in data
                    where
                        (counter.CalculationDate.AddMonths(2) >= counter.VerificationDateNext &&
                         counter.VerificationDateNext != default(DateTime))
                    select "Прибор учёта с заводским номером " + counter.MeterDeviceNumber + " по услуге '" + counter.ServiceName + "'")
                    .ToList();

                return View(data);
            }
        }

        /// <summary>
        /// Сохранить показания приборов учета лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> SaveCounters(decimal enteredValue, long meterReadingId)
        {
            var dataResult = await OhConfigurator.Container.Resolve<IAbonentService>()
                .EnterMeterReading(meterReadingId, enteredValue,EInformationSource.Abonent);

            //TODO: допилить проверку и возврат ответа пользователю
            if (!dataResult.Success)
            {
                ViewBag.ErrorMessageText = dataResult.InfoMessage;
            }
            return RedirectToAction("Counters");
        }

        /// <summary>
        /// Платежи лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Payments()
        {
            var userIdentityid = User.Identity.GetUserId<int>();
            using (var context = new ApplicationDbContext())
            {
                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return RedirectToAction("AddPersonalAccount");
                if (abonents.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                var abonent = abonents.Single();


                var data = (await OhConfigurator.Container.Resolve<IAbonentService>()
                    .GetPayments(abonent.PersonalAccountId)).Content;

                return View(data);
            }
        }

        /// <summary>
        /// Социальные выплаты лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> SocialPayments()
        {
            var userIdentityid = User.Identity.GetUserId<int>();
            using (var context = new ApplicationDbContext())
            {
                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return RedirectToAction("AddPersonalAccount");
                if (abonents.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                var abonent = abonents.Single();


                var data = (await OhConfigurator.Container.Resolve<IAbonentService>()
                    .GetSocialPayments(abonent.PersonalAccountId)).Content;

                return View(data);
            }
        }
        
        /// <summary>
        /// Новое сообщения
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public int CountNewMessage()
        {
            return 0;
        }

        /// <summary>
        /// Новый опрос
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public int CountNewPoll()
        {
            return 0;
        }


        /// <summary>
        /// Заявки лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Claims(FormCollection formValue, EClaimStatus? claimStatus)
        {
            var userIdentityid = User.Identity.GetUserId<int>();
            using (var context = new ApplicationDbContext())
            {
                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return RedirectToAction("AddPersonalAccount");
                if (abonents.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                var abonent = abonents.Single();

                long claimKindId;
                long.TryParse(formValue["ClaimKinds"], out claimKindId);

                ViewData["ClaimKinds"] =
                    context.DictClaimKind
                        .Where(x => x.IsVisible)
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.Name,
                                Value = x.Id.ToString(),
                                Selected = x.Id == claimKindId
                            })
                        .OrderBy(x => x.Text)
                        .ToList();


                var data = (await OhConfigurator.Container.Resolve<IAbonentService>()
                    .GetClaims(abonent.PersonalAccountId,
                        claimKindId: claimKindId,
                        claimStatus: claimStatus))
                    .Content
                    //костыль, так как EF не может прикастить enum
                    .WhereIf(claimStatus.HasValue, x => x.ClaimStatus == claimStatus)
                    .OrderByDescending(x => x.CreationTime);

                ViewBag.SelectedStatus = claimStatus;
                ViewBag.SelectedStatusName = claimStatus?.GetDisplayName();
                return View(data);
            }
        }

        /// <summary>
        /// Создать заявку
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateClaim(FormCollection formValue)
        {
            using (var context = new ApplicationDbContext())
            {
                ViewData["DictClaimKindId"] =
                    context.DictClaimKind.Where(x => x.IsVisible)
                    .Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        })
                        .OrderBy(x=>x.Text)
                        .ToList();
            }
            return View();
        }



        // POST: Claim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateClaim(
            //[Bind(
            //    Include =
            //        "Id,PersonalAccountId,ApartmentFullAddress,DictClaimKindId,Description,ContactPhoneNumber,ClaimStatus,CompleteDate,EmployeeId,OrganizationId,CreationTime,ChangedTime"
            //    )]
        ClaimViewModel claimViewModel, HttpPostedFileBase inputFile)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var userIdentityid = User.Identity.GetUserId<int>();

                        var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                        if (!abonents.Any()) return RedirectToAction("AddPersonalAccount");
                        if (abonents.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                        var abonent = abonents.Single();

                    var claim = new AbonentClaim
                    {
                        OrganizationId = abonent.OrganizationId,
                        CreatorId = abonent.Id,
                        ApartmentFullAddress = abonent.ApartmentFullAddress,
                        ChangedTime = DateTime.Now,
                        CreationTime = DateTime.Now,
                        ContactPhoneNumber = claimViewModel.ContactPhoneNumber,
                        PersonalAccountId = abonent.PersonalAccountId,
                        DictClaimKindId = claimViewModel.DictClaimKindId,
                        Description = claimViewModel.Description,
                        ClaimStatus = EClaimStatus.Created
                    };

                    if (inputFile != null && inputFile.ContentLength > 0)
                    {
                        var fileStorageManager =
                            OhConfigurator.Container.Resolve<IFileStorageManager>();
                        claim.ImportedFileId = (await fileStorageManager.SaveImportedFile(inputFile, userIdentityid, EImportType.AbonentFiles)).Id;
                    }

                    context.AbonentClaim.Add(claim);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Claims");
                }
                
                ViewData["DictClaimKindId"] =
                    context.DictClaimKind
                    .Where(x => x.IsVisible)
                    .Select(x =>
                        new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString(),
                            Selected = x.Id == claimViewModel.DictClaimKindId
                        }).ToList();

                return View(claimViewModel);
            }
        }

        // GET: Abonent/ClaimDetails/5
        public async Task<ActionResult> ClaimDetails(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();

                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return RedirectToAction("AddPersonalAccount");
                if (abonents.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                var abonent = abonents.Single();

                //отображаем заявки созданные только для ЛС абонента
                var claim = await context.AbonentClaim
                    .Include(x => x.Employee)
                    .Include(x => x.DictClaimKind)
                    .Include(x=>x.ImportedFile)
                    .Include(x=>x.ClosedByEmployee)
                    .FirstOrDefaultAsync(x => x.Id == id && x.PersonalAccountId == abonent.PersonalAccountId);

                if (claim == null)
                {
                    return HttpNotFound();
                }

                //список комментариев к заявке
                var comments = await context.AbonentClaimComment
                    .Where(x => x.AbonentClaimId == claim.Id)
                    .Include(x=>x.AbonentClaim)
                    .Include(x => x.AbonentClaim.Creator)
                    .Include(x => x.AbonentClaim.Employee)
                    .Select(x=>new AbonentClaimCommentViewModel
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        Creator = x.AbonentClaim.Creator,
                        Employee = x.Employee,
                        InformationSource = x.InformationSource,
                        CreationTime = x.CreationTime
                    })
                    .OrderBy(x => x.Id)
                    .ToListAsync();

                return View(new ClaimViewModel
                {
                    Id = claim.Id,
                    ClaimStatus = claim.ClaimStatus,
                    ApartmentFullAddress = claim.ApartmentFullAddress,
                    CreationTime = claim.CreationTime,
                    ChangedTime = claim.ChangedTime,
                    CompleteDate = claim.CompleteDate,
                    ContactPhoneNumber = claim.ContactPhoneNumber,
                    Description = claim.Description,
                    DictClaimKind = claim.DictClaimKind,
                    Employee = claim.Employee,
                    CommentList = comments,
                    ImportedFileId = claim.ImportedFile?.ExchangeFileId,
                    Accepted = claim.Accepted,
                    Rating = claim.Rating,
                    ClosedByEmployee = claim.ClosedByEmployee
                });
            }
        }
        
        // POST: Claim/CommentClaim
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CommentClaim(AbonentClaimComment abonentClaimViewModel, HttpPostedFileBase inputFile)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var userIdentityid = User.Identity.GetUserId<int>();

                    var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                    if (!abonents.Any()) return RedirectToAction("AddPersonalAccount");
                    if (abonents.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                    var abonent = abonents.Single();

                    var claimComment = new AbonentClaimComment
                    {
                        OrganizationId = abonent.OrganizationId,
                        AbonentClaimId = abonentClaimViewModel.AbonentClaimId,
                        Comment = abonentClaimViewModel.Comment,
                        ChangedTime = DateTime.Now,
                        CreationTime = DateTime.Now,
                        InformationSource = EInformationSource.Abonent
                    };

                    if (inputFile != null && inputFile.ContentLength > 0)
                    {
                        var fileStorageManager =
                            OhConfigurator.Container.Resolve<IFileStorageManager>();
                        claimComment.ImportedFileId = (await fileStorageManager.SaveImportedFile(inputFile, userIdentityid, EImportType.AbonentFiles)).Id;
                    }

                    context.AbonentClaimComment.Add(claimComment);
                    await context.SaveChangesAsync();
                }

                return RedirectToAction("ClaimDetails", new { id = abonentClaimViewModel.AbonentClaimId });
            }
        }
        
        // POST: Claim/CloseClaim
        public async Task<ActionResult> CloseClaim()
        {
            using (var context = new ApplicationDbContext())
            {
                long claimId;
                long.TryParse(Request["AbonentClaimId"], out claimId);


                int rating;
                int.TryParse(Request["Rating"], out rating);

                var claim = await context.AbonentClaim.FindAsync(claimId);
                if (claim == null)
                    return HttpNotFound();
                
                var userIdentityid = User.Identity.GetUserId<int>();

                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return RedirectToAction("AddPersonalAccount");
                if (abonents.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");

                var abonent = abonents.Single();

                if (claim.CreatorId != abonent.Id)
                    return HttpNotFound();

                claim.Rating = rating;
                claim.Accepted = true;
                claim.CompleteDate = DateTime.Now;
                claim.ClaimStatus = EClaimStatus.Closed;

                await context.SaveChangesAsync();


                return RedirectToAction("ClaimDetails", new {id = claimId });
            }
        }

        //// GET: Claim/Edit/5
        //public async Task<ActionResult> EditClaim(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClaimViewModel claimViewModel = await db.Claim.FindAsync(id);
        //    if (claimViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.DictClaimKindId = new SelectList(db.DictClaimKind, "Id", "Name", claimViewModel.DictClaimKindId);
        //    ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Id", claimViewModel.EmployeeId);
        //    ViewBag.OrganizationId = new SelectList(db.Organization, "Id", "FullName", claimViewModel.OrganizationId);
        //    ViewBag.PersonalAccountId = new SelectList(db.PersonalAccount, "Id", "ApartmentFullAddress", claimViewModel.PersonalAccountId);
        //    return View(claimViewModel);
        //}

        //// POST: Claim/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> EditClaim(
        ////    [Bind(Include = "Id,PersonalAccountId,ApartmentFullAddress,DictClaimKindId,Description,ContactPhoneNumber,ClaimStatus,CompleteDate,EmployeeId,OrganizationId,CreationTime,ChangedTime")]
        //ClaimViewModel claimViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(claimViewModel).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.DictClaimKindId = new SelectList(db.ClaimKinds, "Id", "Name", claimViewModel.DictClaimKindId);
        //    ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Id", claimViewModel.EmployeeId);
        //    ViewBag.OrganizationId = new SelectList(db.Organization, "Id", "FullName", claimViewModel.OrganizationId);
        //    ViewBag.PersonalAccountId = new SelectList(db.PersonalAccount, "Id", "ApartmentFullAddress", claimViewModel.PersonalAccountId);
        //    return View(claimViewModel);
        //}

        //// GET: Claim/Delete/5
        //public async Task<ActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ClaimViewModel claimViewModel = await db.Claim.FindAsync(id);
        //    if (claimViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(claimViewModel);
        //}

        //// POST: Claim/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(long id)
        //{
        //    ClaimViewModel claimViewModel = await db.Claim.FindAsync(id);
        //    db.Claim.Remove(claimViewModel);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        /*
       public ActionResult DetailClaims(int Id)
       {

           using (var context = new OhDbContext())
           {
               var types = context.RequestType.ToList();
               var statuses = context.Status.ToList();
               var requests = context.Requests.Where(x => x.Id == Id).ToList();

               var data =
                   (from request in requests
                       join type in types
                           on request.RequestType.Id equals type.Id
                       join status in statuses
                           on request.Status.Id equals status.Id

                       select new Requests()
                       {
                           Id = request.Id,
                           AnswerTxt = request.AnswerTxt,
                           RequestType = type,
                           RequestTxt = request.RequestTxt,
                           DateCreate = request.DateCreate,
                           DateClose = request.DateClose,
                           Status = status
                       }
                       ).ToList();

               //var data = DependencyProvider
               //       .Resolve<DataManagersFactory>()
               //       .GetPersonalAccountDataManager().GetRequest(Id);

               foreach (Requests field in data)
               {
                   ViewData["RequesttypeDetail"] = from RequestType in DependencyProvider
                       .Resolve<DataManagersFactory>()
                       .GetPersonalAccountDataManager().GetRequestType()
                       select
                           new SelectListItem
                           {
                               Text = RequestType.Name,
                               Selected = (RequestType.Id == field.RequestType.Id)
                           };
               }

               if (data != null)
                   return PartialView(data);
               return HttpNotFound();
           }

       }



       /// <summary>
       /// Опросы
       /// </summary>
       /// <returns></returns>
       [Authorize]
       public ActionResult Interviews(FormCollection formValue)
       {
           var userIdentityid = User.Identity.GetUserId<int>();
           using (var context = new OhDbContext())
           {

               var viewClosed = Convert.ToString(formValue["Enable"]);
               List<SelectListItem> li = new List<SelectListItem>();
               li.Add(new SelectListItem {Text = "Только открытые", Selected = ("Только открытые" == viewClosed)});
               ViewData["Enable"] = li;

               var _Pools = string.IsNullOrEmpty(viewClosed)
                   ? context.Pools.Where(x => x.Employee != null).ToList()
                   : context.Pools.Where(x => x.Employee != null && x.PoolStatus.Id != 2).ToList();
               var _PoolVariants = string.IsNullOrEmpty(viewClosed)
                   ? context.PoolVariants.ToList()
                   : context.PoolVariants.Where(x => x.Pool.PoolStatus.Id != 2).ToList();
               var _PoolStatus = string.IsNullOrEmpty(viewClosed)
                   ? context.PoolStatus.ToList()
                   : context.PoolStatus.Where(x => x.Id != 2).ToList();
               var _PoolRecords = string.IsNullOrEmpty(viewClosed)
                   ? context.PoolData.ToList()
                   : context.PoolData.Where(x => x.Pool.PoolStatus.Id != 2).ToList();
               var _Employees = context.Employee.ToList();

               var data =
                   (from Pool in _Pools
                       //join PoolRecord in _PoolRecords
                       //                      on Pool.Id equals PoolRecord.Pool.Id into temp
                       //from t in temp.DefaultIfEmpty()
                       //join PoolVariant in _PoolVariants
                       // on Pool.Id equals PoolVariant.Pool.Id
                       join PoolStatus in _PoolStatus
                           on Pool.PoolStatus.Id equals PoolStatus.Id
                       join Employee in _Employees
                           on Pool.Employee.Id equals Employee.Id

                       select new Pools()
                       {
                           Id = Pool.Id,
                           Name = Pool.Name,
                           Note = Pool.Note,
                           DateCreate = Pool.DateCreate,
                           DateFinish = Pool.DateFinish,
                           Employee = Pool.Employee,
                           PoolStatus = PoolStatus,
                           CountVote = context.PoolData.Count(x => x.Pool.Id == Pool.Id),
                           IsVote =
                               context.PoolData.Count(
                                   x => x.Pool.Id == Pool.Id && x.Abonent.UserIdentity == userIdentityid)
                       }
                       ).ToList();

               var pooldata =
                   (from PoolRecord in _PoolRecords
                       join PoolVariant in _PoolVariants
                           on PoolRecord.PoolVariant.Id equals PoolVariant.Id
                       join Pool in _Pools
                           on PoolRecord.Pool.Id equals Pool.Id

                       select new PoolData()
                       {
                           Id = PoolRecord.Id,
                           DatePool = PoolRecord.DatePool,
                           Abonent = PoolRecord.Abonent,
                           Pool = Pool,
                           PoolVariant = PoolVariant
                       }
                       ).ToList();


               ViewBag.PoolData = pooldata;
               ViewBag.PoolVariants = _PoolVariants;

               return View(data);
           }
       }

       [HttpPost]
       public ActionResult SetVote(int PoolId, int PoolVarianId)
       {
           var userIdentityid = User.Identity.GetUserId<int>();

           using (var context = new OhDbContext())
           {
               //создам сущность загруженного файла
               var PoolData = new PoolData
               {
                   Abonent = context.Abonents.FirstOrDefault(x => x.UserIdentity == userIdentityid),
                   DatePool = DateTime.Now,
                   Pool = context.Pools.FirstOrDefault(x => x.Id == PoolId),
                   PoolVariant = context.PoolVariants.FirstOrDefault(x => x.Id == PoolVarianId)
                   //,
                   //Customer = 
               };
               context.PoolData.Add(PoolData);
               context.SaveChangesAsync();
           }

           return RedirectToAction("Interviews");
       }


       public ActionResult DetailPool(int Id)
       {

           using (var context = new OhDbContext())
           {

               var _Pools = context.Pools.Where(x => x.Id == Id).ToList();
               var _PoolStatus = context.PoolStatus.ToList();
               var _Employees = context.Employee.ToList();

               var data =
                   (from Pool in _Pools
                       join PoolStatus in _PoolStatus
                           on Pool.PoolStatus.Id equals PoolStatus.Id
                       join Employee in _Employees
                           on Pool.Employee.Id equals Employee.Id

                       select new Pools()
                       {
                           Id = Pool.Id,
                           Name = Pool.Name,
                           Note = Pool.Note,
                           DateCreate = Pool.DateCreate,
                           DateFinish = Pool.DateFinish,
                           Employee = Pool.Employee,
                           PoolStatus = PoolStatus
                       }
                       ).ToList();

               if (data != null)
                   return PartialView(data);
               return HttpNotFound();
           }

       }

       public ActionResult AboutPool(int Id)
       {

           using (var context = new OhDbContext())
           {

               var _Pools = context.Pools.Where(x => x.Id == Id).ToList();
               var _PoolStatus = context.PoolStatus.ToList();
               var _Employees = context.Employee.ToList();

               var data =
                   (from Pool in _Pools
                       join PoolStatus in _PoolStatus
                           on Pool.PoolStatus.Id equals PoolStatus.Id
                       join Employee in _Employees
                           on Pool.Employee.Id equals Employee.Id

                       select new Pools()
                       {
                           Id = Pool.Id,
                           Name = Pool.Name,
                           Note = Pool.Note,
                           DateCreate = Pool.DateCreate,
                           DateFinish = Pool.DateFinish,
                           Employee = Pool.Employee,
                           PoolStatus = PoolStatus
                       }
                       ).ToList();

               if (data != null)
                   return PartialView(data);
               return HttpNotFound();
           }

       }

       public ActionResult CreatePool()
       {
           return PartialView(null);
       }

       [HttpPost]
       public ActionResult InsertPool(string rname, string rcomment, string rdate, List<string> arr)
       {
           var userIdentityid = User.Identity.GetUserId<int>();

           using (var context = new OhDbContext())
           {
               //создам сущность загруженного файла
               var Pool = new Pools
               {
                   Name = rname,
                   Note = rcomment,
                   DateCreate = DateTime.Now,
                   DateFinish = Convert.ToDateTime(rdate),
                   Employee = context.Employee.FirstOrDefault(x => x.UserIdentityId == userIdentityid),
                   PoolStatus = context.PoolStatus.FirstOrDefault(x => x.Id == 0)
               };
               context.Pools.Add(Pool);
               context.SaveChangesAsync();

               //при сохранении у объекта заполнится Id
               var PoolId = Pool.Id;

               for (int i = 0; i < arr.Count; i++)
               {
                   var PoolVariant = new PoolVariants
                   {
                       Name = arr[i],
                       Pool = context.Pools.FirstOrDefault(x => x.Id == PoolId)
                   };
                   context.PoolVariants.Add(PoolVariant);
                   context.SaveChangesAsync();
               }
           }

           return RedirectToAction("Interviews");
       }

       [HttpPost]
       public ActionResult UpdatePool(string rname, string rcomment, string rdate, string rid)
       {
           var userIdentityid = User.Identity.GetUserId<int>();

           using (var context = new OhDbContext())
           {
               int Id = Convert.ToInt32(rid);
               //создам сущность загруженного файла
               var Pool = context.Pools.FirstOrDefault(x => x.Id == Id && x.Employee.UserIdentityId == userIdentityid);


               Pool.Name = rname;
               Pool.Note = rcomment;
               Pool.DateFinish = Convert.ToDateTime(rdate);

               context.SaveChangesAsync();
           }

           return RedirectToAction("Interviews");
       }

       [HttpPost]
       public ActionResult DeletePool(int rid)
       {
           var userIdentityid = User.Identity.GetUserId<int>();

           using (var context = new OhDbContext())
           {
               //создам сущность загруженного файла
               var Pool = context.Pools.FirstOrDefault(x => x.Id == rid && x.Employee.UserIdentityId == userIdentityid);
               if (Pool != null)
               {
                   var PoolVariant = context.PoolVariants.Where(x => x.Pool.Id == rid).ToList();
                   var PoolData = context.PoolData.Where(x => x.Pool.Id == rid).ToList();

                   context.PoolData.RemoveRange(PoolData);
                   context.SaveChangesAsync();
                   context.PoolVariants.RemoveRange(PoolVariant);
                   context.SaveChangesAsync();
                   context.Pools.Remove(Pool);
                   context.SaveChangesAsync();
               }
           }

           return RedirectToAction("Interviews");
       }

       /// <summary>
       /// Сообщения
       /// </summary>
       /// <returns></returns>
       [Authorize]
       public ActionResult Notifications()
       {
           var userIdentityid = User.Identity.GetUserId<int>();
           using (var context = new OhDbContext())
           {

               var Sends = context.Sends.ToList();
               var AbobentReceiver = context.Abonents.Where(x => x.UserIdentity == userIdentityid).ToList();
               var EmployeeCreator = context.Employee.ToList();

               var data =
                   (from Send in Sends
                       join Abobent in AbobentReceiver
                           on Send.AbobentReceiver.Id equals Abobent.Id
                       join Employee in EmployeeCreator
                           on Send.EmployeeCreator.Id equals Employee.Id

                       select new Sends()
                       {
                           Id = Send.Id,
                           SendSms = Send.SendSms,
                           SendEmail = Send.SendEmail,
                           AbobentReceiver = Abobent,
                           EmployeeCreator = Employee,
                           DateCreate = Send.DateCreate,
                           DateSend = Send.DateSend,
                           ErrorTxt = Send.ErrorTxt,
                           IsRead = Send.IsRead
                       }
                       ).ToList();

               ViewBag.CountNewMessage = data.Count();

               return View(data);

           }
       }

       /// <summary>
       /// Новое сообщения
       /// </summary>
       /// <returns></returns>
       [Authorize]
       public Int32 CountNewMessage()
       {
           return 1;
           var userIdentityid = User.Identity.GetUserId<int>();
           using (var context = new OhDbContext())
           {

               var Sends = context.Sends.Where(x => x.IsRead == false).ToList();
               var AbobentReceiver = context.Abonents.Where(x => x.UserIdentity == userIdentityid).ToList();
               var EmployeeCreator = context.Employee.ToList();

               var data =
                   (from Send in Sends
                       join Abobent in AbobentReceiver
                           on Send.AbobentReceiver.Id equals Abobent.Id
                       join Employee in EmployeeCreator
                           on Send.EmployeeCreator.Id equals Employee.Id

                       select new Sends()
                       {
                           Id = Send.Id,
                           SendSms = Send.SendSms,
                           SendEmail = Send.SendEmail,
                           AbobentReceiver = Abobent,
                           EmployeeCreator = Employee,
                           DateCreate = Send.DateCreate,
                           DateSend = Send.DateSend,
                           ErrorTxt = Send.ErrorTxt,
                           IsRead = Send.IsRead
                       }
                       ).ToList();


               return data.Count();

           }
       }

       public ActionResult ReadMessage(int Id)
       {
           var userIdentityid = User.Identity.GetUserId<int>();

           using (var context = new OhDbContext())
           {
               //создам сущность загруженного файла
               var Send =
                   context.Sends.FirstOrDefault(x => x.Id == Id && x.AbobentReceiver.UserIdentity == userIdentityid);
               Send.IsRead = true;
               context.SaveChangesAsync();
           }

           return RedirectToAction("Notifications");
       }
       /// <summary>
       /// Адрес лицевого счета
       /// </summary>
       /// <returns></returns>
       [Authorize]
       public String GetPersonalAccAddress()
       {
           var userIdentityid = User.Identity.GetUserId<int>();
           using (var context = new OhDbContext())
           {
               //характеристики лицевого счета
               var personalAccountInfo = DependencyProvider
                   .Resolve<DataManagersFactory>()
                   .GetPersonalAccountDataManager().GetPersonalAccountInfo(userIdentityid);

               return personalAccountInfo.FullAddress;

           }

       }
       */
    }
}
