using System.Data.Entity;
using System.Threading.Tasks;
using FastReport;
using OhDataManager.Enums;

namespace WebAppAuth.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data;
    using System.Globalization;
    using Context;
    using DependencyResolver;
    using OhDataManager;
    using FastReport.Web;
    using System.Web.UI.WebControls;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Отчеты
    /// </summary>
    [Authorize]
    public class ReportController : BaseController
    {

        private readonly WebReport _webReport = new WebReport();

        /// <summary>
        /// Конфигурирование отчета
        /// </summary>
        /// <returns></returns>
        private void SetReport()
        {
            _webReport.Width = Unit.Percentage(100);
            _webReport.Height = Unit.Percentage(100);
            _webReport.ToolbarIconsStyle = ToolbarIconsStyle.Black;
            _webReport.ToolbarStyle = ToolbarStyle.Large;
            _webReport.LocalizationFile = "~/Reports/Localization/Russian.frl";
            _webReport.PrintInBrowser = true;
            _webReport.PrintInPdf = true;
            _webReport.ShowExports = true;
            _webReport.ShowPrint = true;
            _webReport.SinglePage = true;
            _webReport.Layers = true;
            _webReport.EmbedPictures = false;
            _webReport.XlsxPageBreaks = false;
            _webReport.XlsxSeamless = true;
        }

        /// <summary>
        /// Список зарегистрированных пользователей
        /// </summary>
        /// <param name="formValue"></param>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleEmployee + ", " + AppConstants.RoleDirector)]
        public ActionResult SetReportListOfUsers(FormCollection formValue)
        {
            var reportPath = GetReportPath();
            var start = new DateTime(1900, 1, 1);
            var end = new DateTime(3000, 1, 1);
            if (formValue["reservation"] != null)
            {
                var range = formValue["reservation"];
                var format = "MM/dd/yyyy";
                var stringSeparators = new[] {" - "};
                var split = range.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                var provider = CultureInfo.InvariantCulture;

                if (split.Length == 2)
                {
                    start = DateTime.ParseExact(split[0], format, provider);
                    end = DateTime.ParseExact(split[1], format, provider);
                }
            }

            SetReport();
            var dataSet = new DataSet();
            DataTable dt;

            using (var context = new ApplicationDbContext())
            {
                dt = context.Abonent
                    .Where(x => x.CreationTime >= start && x.CreationTime <= end)
                    .Select(x =>
                        new
                        {
                            x.PaymentCode,
                            x.Surname,
                            x.Name,
                            x.Patronymic,
                            x.ApplicationUser.Email,
                            x.CreationTime
                        })
                    .OrderByDescending(x => x.CreationTime)
                    .ToList()
                    .ToDataTable();
            }

            dt.TableName = "Users";
            dataSet.Tables.Add(dt);
            _webReport.Report.RegisterData(dataSet, "NorthWind");
            _webReport.Report.Load(reportPath + "ListOfUsers.frx");
            _webReport.CurrentTab.Name = "Список зарегистрированных пользователей";
            ViewBag.TypeFilter = 1;
            ViewBag.Reservation = formValue["reservation"];
            ViewBag.WebReport = _webReport;
            return View("Report");
        }

        /// <summary>
        /// Список показаний ИПУ
        /// </summary>
        /// <param name="formValue"></param>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleEmployee + ", " + AppConstants.RoleDirector)]
        public ActionResult SetReportListOfIpuVals(FormCollection formValue)
        {
            var reportPath = GetReportPath();
            SetReport();

            //var homeAddress = Convert.ToString(formValue["Houses"]);
            //ViewData["Houses"] = from parameters in DependencyProvider
            //    .Resolve<DataManagersFactory>()
            //    .GetEmployeeDataManager().GetHouses()
            //                     select new SelectListItem { Text = parameters.Houses, Selected = (parameters.Houses == homeAddress) };

            //var kvar = Convert.ToString(formValue["Kvars"]);
            //ViewData["Kvars"] = from parameters in DependencyProvider
            //    .Resolve<DataManagersFactory>()
            //    .GetEmployeeDataManager().GetListOfApartments(homeAddress)
            //                    select new SelectListItem { Text = parameters.Kvars, Selected = (parameters.Kvars == kvar) };

            using (var context = new ApplicationDbContext())
            {

                var userIdentityid = User.Identity.GetUserId<int>();
                var employee = context.Employee.First(x => x.ApplicationUserId == userIdentityid);


                long houseId;
                long.TryParse(formValue["Houses"], out houseId);

                ViewData["Houses"] =
                    context.House
                        .Where(x => x.OrganizationId == employee.OrganizationId)
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.HouseFullAddress,
                                Value = x.Id.ToString(),
                                Selected = x.Id == houseId
                            })
                        .ToList();

                long personalAccountId;
                long.TryParse(formValue["Kvars"], out personalAccountId);



                ViewData["Kvars"] =
                    context.PersonalAccount
                        .Where(x => x.OrganizationId == employee.OrganizationId &&
                                    x.HouseId == houseId)
                        //.OrderBy(x => R.Regexp(@"\d+", x.ApartmentNumber))
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.ApartmentAddress,
                                Value = x.Id.ToString(),
                                Selected = x.Id == personalAccountId
                            })
                        .ToList();

                var start = new DateTime(1900, 1, 1);
                var end = new DateTime(3000, 1, 1);
                if (formValue["reservation"] != null)
                {
                    var range = formValue["reservation"];
                    var format = "MM/dd/yyyy";
                    var stringSeparators = new[] {" - "};
                    var split = range.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    var provider = CultureInfo.InvariantCulture;

                    if (split.Length == 2)
                    {
                        start = DateTime.ParseExact(split[0], format, provider);
                        end = DateTime.ParseExact(split[1], format, provider);
                    }
                }

                ViewBag.Reservation = formValue["reservation"];
                var allvals = false;
                if (formValue["allvals"] != null)
                {
                    var bvals = formValue["allvals"];
                    var bsplit = bvals.Split(',');

                    allvals = Convert.ToBoolean(bsplit[0].ToLower());
                }
                ViewBag.AllVals = allvals;

                var personalAccountMeterReading =
                    context.PersonalAccountMeterReading.Where(x =>
                        x.OrganizationId == employee.OrganizationId &&
                        x.HouseId == houseId &&
                        x.EnteredValue != null);

                if (personalAccountId != 0)
                    personalAccountMeterReading =
                        personalAccountMeterReading.Where(x => x.PersonalAccountId == personalAccountId);

                personalAccountMeterReading =
                    personalAccountMeterReading.Where(x => x.VerificationDate >= start && x.VerificationDate <= end);

                if (!allvals)
                    personalAccountMeterReading = personalAccountMeterReading.Where(x => x.ExportedFileId == null);

                DataTable dt = personalAccountMeterReading.ToList().ToDataTable();

                var dataSet = new DataSet();
                //DependencyProvider
                //   .Resolve<DataManagersFactory>()
                //   .GetEmployeeDataManager().GetReportCountersOrd(homeAddress, kvar, start, end, allvals);
                dt.TableName = "Counters";
                dataSet.Tables.Add(dt);
                _webReport.Report.RegisterData(dataSet, "CountersOrd");
                _webReport.Report.Load(reportPath + "ListOfIPUVals.frx");
                _webReport.CurrentTab.Name = "Список показаний ИПУ";
                ViewBag.TypeFilter = 2;
                ViewBag.WebReport = _webReport;
                return View("Report");
            }
        }

        /// <summary>
        /// Список показаний ОДПУ
        /// </summary>
        /// <param name="formValue"></param>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleEmployee + ", " + AppConstants.RoleDirector)]
        public ActionResult SetReportListOfOdpuVals(FormCollection formValue)
        {
            var reportPath = GetReportPath();
            SetReport();
            //var homeAddress = Convert.ToString(formValue["Houses"]);
            //ViewData["Houses"] = from parameters in DependencyProvider
            //    .Resolve<DataManagersFactory>()
            //    .GetEmployeeDataManager().GetHouses()
            //                     select new SelectListItem { Text = parameters.Houses, Selected = (parameters.Houses == homeAddress) };
            using (var context = new ApplicationDbContext())
            {

                var userIdentityid = User.Identity.GetUserId<int>();
                var employee = context.Employee.First(x => x.ApplicationUserId == userIdentityid);


                long houseId;
                long.TryParse(formValue["Houses"], out houseId);

                ViewData["Houses"] =
                    context.House
                        .Where(x => x.OrganizationId == employee.OrganizationId)
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.HouseFullAddress,
                                Value = x.Id.ToString(),
                                Selected = x.Id == houseId
                            })
                        .ToList();



                var start = new DateTime(1900, 1, 1);
                var end = new DateTime(3000, 1, 1);
                if (formValue["reservation"] != null)
                {
                    var range = formValue["reservation"];
                    var format = "MM/dd/yyyy";
                    var stringSeparators = new[] {" - "};
                    var split = range.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    var provider = CultureInfo.InvariantCulture;

                    if (split.Length == 2)
                    {
                        start = DateTime.ParseExact(split[0], format, provider);
                        end = DateTime.ParseExact(split[1], format, provider);
                    }
                }

                ViewBag.Reservation = formValue["reservation"];
                var allvals = false;
                if (formValue["allvals"] != null)
                {
                    var bvals = formValue["allvals"];
                    var bsplit = bvals.Split(',');

                    allvals = Convert.ToBoolean(bsplit[0].ToLower());
                }
                ViewBag.AllVals = allvals;

                var dataSet = new DataSet();
                //var dt = DependencyProvider
                //       .Resolve<DataManagersFactory>()
                //       .GetEmployeeDataManager().GetReportCountersDomOrd(homeAddress, start, end, allvals);

                var houseMeterReadings = context.HouseMeterReading
                    .Where(
                        x =>
                            x.OrganizationId == employee.OrganizationId &&
                            x.HouseId == houseId &&
                            x.EnteredValue != null);

                houseMeterReadings =
                    houseMeterReadings.Where(x => x.VerificationDate >= start && x.VerificationDate <= end);

                if (!allvals)
                    houseMeterReadings = houseMeterReadings.Where(x => x.ExportedFileId == null);

                DataTable dt = houseMeterReadings.ToList().ToDataTable();
                dt.TableName = "Counters";
                dataSet.Tables.Add(dt);
                _webReport.Report.RegisterData(dataSet, "CountersDomOrd");
                _webReport.Report.Load(reportPath + "ListOfODPUVals.frx");
                _webReport.CurrentTab.Name = "Список показаний ОДПУ";
                ViewBag.TypeFilter = 3;
                ViewBag.WebReport = _webReport;
                return View("Report");
            }
        }

        /// <summary>
        /// Список заявок
        /// </summary>
        /// <param name="formValue"></param>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleEmployee + ", " + AppConstants.RoleDirector)]
        public ActionResult SetReportOfClaims(FormCollection formValue)
        {
            var reportPath = GetReportPath();
            SetReport();

            using (var context = new ApplicationDbContext())
            {
                long claimTypeId;
                long.TryParse(formValue["ClaimKind"], out claimTypeId);

                ViewData["ClaimKind"] =
                    context.DictClaimKind
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.Name,
                                Value = x.Id.ToString(),
                                Selected = x.Id == claimTypeId
                            })
                        .ToList();

                int claimStateId;
                int.TryParse(formValue["ClaimState"], out claimStateId);

                ViewData["ClaimState"] =
                    Enum.GetValues(typeof (EClaimStatus)).Cast<EClaimStatus>()
                        .Select(state => new SelectListItem
                        {
                            Text = state.GetDisplayName(),
                            Value = ((int) state).ToString(),
                            Selected = (int) state == claimStateId
                        }).ToList();



                var start = new DateTime(1900, 1, 1);
                var end = new DateTime(3000, 1, 1);
                if (formValue["reservation"] != null)
                {
                    var range = formValue["reservation"];
                    var format = "MM/dd/yyyy";
                    var stringSeparators = new[] {" - "};
                    var split = range.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    var provider = CultureInfo.InvariantCulture;

                    if (split.Length == 2)
                    {
                        start = DateTime.ParseExact(split[0], format, provider);
                        end = DateTime.ParseExact(split[1], format, provider);
                    }
                }

                var headerDate = string.Empty;

                if (start > new DateTime(1900, 1, 1) && end < new DateTime(3000, 1, 1))
                    headerDate = start == end
                        ? start.ToString("dd.MM.yyyy")
                        : $"c {start.ToString("dd.MM.yyyy")} по  {end.ToString("dd.MM.yyyy")}";

                var dataSet = new DataSet();

                var claims = context.AbonentClaim
                    .Include(x => x.Creator)
                    .Include(x => x.Employee)
                    .Include(x => x.DictClaimKind)
                    .Where(x => x.CreationTime >= start && x.CreationTime <= end);

                var claimKindHeader = "все";
                if (claimTypeId > 0)
                {
                    claims = claims.Where(x => x.DictClaimKindId == claimTypeId);
                    var claimKind = context.DictClaimKind.FirstOrDefault(x => x.Id == claimTypeId);
                    if (claimKind != null)
                        claimKindHeader = claimKind.Name;
                }

                var claimStateHeader = "все";
                if (claimStateId > 0)
                {
                    claims = claims.Where(x => x.ClaimStatus == (EClaimStatus) claimStateId);
                    claimStateHeader = Enum
                        .GetValues(typeof (EClaimStatus))
                        .Cast<EClaimStatus>()
                        .FirstOrDefault(x => (int) x == claimStateId).GetDisplayName();
                }

                DataTable dt = claims
                    .OrderBy(x => x.Id)
                    .ToList()
                    .Select(x =>
                    new
                    {
                        x.Id,
                        DictClaimKindName = x.DictClaimKind?.Name ?? string.Empty,
                        x.CreationTime,
                        Abonent = x.Creator?.Name ?? string.Empty,
                        x.ApartmentFullAddress,
                        Employee = x.Employee?.DisplayName ?? string.Empty,
                        CompleteDate = x.CompleteDate?.ToString("dd.MM.yyyy") ?? "",
                        ClaimStatusText = x.ClaimStatus.GetDisplayName(),
                        Description = x.Description
                    }).ToList().ToDataTable();

                var claimsCount =
                    claims.ToList()
                        .GroupBy(x => x.ClaimStatusText)
                        .Select(x => new {ClaimStatusText = x.Key, Count = x.Count()})
                        .ToList()
                        .ToDataTable();


                dt.TableName = "Claims";
                dataSet.Tables.Add(dt);

                claimsCount.TableName = "ClaimsCount";
                dataSet.Tables.Add(claimsCount);

                _webReport.Report.RegisterData(dataSet, "ReportOfClaims");
                _webReport.Report.SetParameterValue("headerDate", headerDate);
                _webReport.Report.SetParameterValue("claimKindHeader", claimKindHeader);
                _webReport.Report.SetParameterValue("claimStateHeader", claimStateHeader);
                _webReport.Report.Load(reportPath + "ReportOfClaims.frx");
                _webReport.CurrentTab.Name = "Журнал регистрации заявок";
                ViewBag.TypeFilter = 5;
                ViewBag.WebReport = _webReport;
                return View("Report");
            }
        }

        /// <summary>
        /// Печать заказ-наряда
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleEmployee + ", " + AppConstants.RoleWorker)]
        public ActionResult SetPrintingJobOrder(int claimId)
        {
            var reportPath = GetReportPath();
            SetReport();

            var ci = new CultureInfo("ru-RU");

            using (var context = new ApplicationDbContext())
            {
                var claim = context.AbonentClaim.Where(x => x.Id == claimId)
                    .Include(x => x.Organization)
                    .Include(x => x.PersonalAccount)
                    .Include(x => x.PersonalAccount.House)
                    .Include(x => x.DictClaimKind)
                    .Include(x => x.Employee)
                    .ToList()
                    .Select(x => new
                    {
                        x.Id,
                        Organization = x.Organization?.FullName ?? string.Empty,
                        HouseNumber = x.PersonalAccount?.House?.HouseNumber ?? string.Empty,
                        Street = x.PersonalAccount?.House?.Street ?? string.Empty,
                        ApartmentNumber = x.PersonalAccount?.ApartmentNumber ?? string.Empty,
                        TypeClaim = x.DictClaimKind?.Name ?? string.Empty,
                        NumberClaim = $"{x.DictClaimKindId}-{x.Id}",
                        Responsible = x.Employee?.DisplayName ?? string.Empty,
                        Description = x.Description,
                        ClaimDate =
                            $"«{x.CreationTime.Day}» {ci.DateTimeFormat.MonthGenitiveNames[x.CreationTime.Month-1]} {x.CreationTime.Year} г."
                    }).FirstOrDefault();



                var report = new Report();
                report.Load(reportPath + "PrintingJobOrder.frx");
                report.SetParameterValue("Organization", claim.Organization);
                report.SetParameterValue("HouseNumber", claim.HouseNumber);
                report.SetParameterValue("Street", claim.Street);
                report.SetParameterValue("ApartmentNumber", claim.ApartmentNumber);
                report.SetParameterValue("TypeClaim", claim.TypeClaim);
                report.SetParameterValue("NumberClaim", claim.NumberClaim);
                report.SetParameterValue("Responsible", claim.Responsible);
                report.SetParameterValue("ClaimDate", claim.ClaimDate);
                report.SetParameterValue("Description", claim.Description);
                report.Prepare();

                _webReport.CurrentTab.Name = "Наряд заказ";
                _webReport.Report = report;
                ViewBag.TypeFilter = 6;
                ViewBag.WebReport = _webReport;
                return View("Report");
            }
        }

        /// <summary>
        /// Список оплат
        /// </summary>
        /// <param name="formValue"></param>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleEmployee + ", " + AppConstants.RoleDirector)]
        public ActionResult SetReportListOfPayments(FormCollection formValue)
        {
            var reportPath = GetReportPath();
            SetReport();
            var start = new DateTime(1900, 1, 1);
            var end = new DateTime(3000, 1, 1);
            if (formValue["reservation"] != null)
            {
                var range = formValue["reservation"];
                var format = "MM/dd/yyyy";
                var stringSeparators = new[] {" - "};
                var split = range.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                var provider = CultureInfo.InvariantCulture;

                if (split.Length == 2)
                {
                    start = DateTime.ParseExact(split[0], format, provider);
                    end = DateTime.ParseExact(split[1], format, provider);
                }
            }

            var dataSet = new DataSet();
            DataTable dt;

            using (var context = new ApplicationDbContext())
            {
                dt = context.UniPay
                    .Where(
                        x =>
                            x.CreationTime >= start && x.CreationTime <= end &&
                            String.IsNullOrEmpty(x.Currency.ToString()) == false)
                    .Select(x =>
                        new
                        {
                            x.CustomerIdp,
                            x.Address,
                            x.SubtotalP,
                            x.CreationTime,
                            x.ShopIdp,
                            x.Currency,
                            x.Email,
                            x.MiddleName,
                            x.FirstName,
                            x.LastName
                        })
                    .ToList()
                    .ToDataTable();
            }

            dt.TableName = "Payments";
            dataSet.Tables.Add(dt);
            _webReport.Report.RegisterData(dataSet, "AccountPayment");
            _webReport.Report.Load(reportPath + "ListOfPayments.frx");
            _webReport.CurrentTab.Name = "Список оплат";
            ViewBag.TypeFilter = 4;
            ViewBag.Reservation = formValue["reservation"];
            ViewBag.WebReport = _webReport;
            return View("Report");
        }

        /// <summary>
        /// Счет фактура
        /// </summary>
        [Authorize(Roles = AppConstants.RoleAbonent)]
        public async Task<ActionResult> GetInvoice()
        {
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();

                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                var abonent = abonents.Single();

                var calculationDate =
                    context.CalculationMonth.Single(x => x.OrganizationId == abonent.OrganizationId && x.IsCurrent)
                        .CalculationDate;

                DataTable dt = (await DependencyProvider
                    .Resolve<DataManagersFactory>()
                    .GetPersonalAccountDataManager()
                    .GetPersonalAccountAccruals(abonent.PersonalAccountId, calculationDate)).ToList().ToDataTable();

                DataTable dtcounters = context.PersonalAccountMeterReading
                    .Where(
                        x =>
                            x.PersonalAccountId == abonent.PersonalAccountId &&
                            x.CalculationDate == calculationDate)
                    .ToList().ToDataTable();

                DataTable dtrequisite = context.PersonalAccountPaymentRequisite
                    .Where(
                        x =>
                            x.PersonalAccountId == abonent.PersonalAccountId &&
                            x.CalculationDate == calculationDate)
                    .ToList().ToDataTable();

                var reportPath = GetReportPath();


                SetReport();
                var dataSet = new DataSet();

                dt.TableName = "Accruals";
                dataSet.Tables.Add(dt);

                dtcounters.TableName = "Counters";
                dataSet.Tables.Add(dtcounters);

                dtrequisite.TableName = "Requisite";
                dataSet.Tables.Add(dtrequisite);

                _webReport.Report.RegisterData(dataSet, "Invoice");

                //_webReport.ReportFile = reportPath + "Invoice.frx";

                // prepare report
                //_webReport.Report.Prepare();
                _webReport.Report.Load(reportPath + "Invoice.frx");
                //ViewBag.TypeFilter = 5;
                ViewBag.WebReport = _webReport;
                return View("Report");

                // save file in stream
                //Stream stream = new MemoryStream();
                //_webReport.Report.Export(new PDFExport(), stream);
                //stream.Position = 0;
                //// return stream in browser
                //return File(stream, "application/zip", "Invoice.pdf");
            }
        }

        /// <summary>
        /// Путь к шаблонам
        /// </summary>
        /// <returns></returns>
        private string GetReportPath()
        {
            return Server.MapPath("~/Reports/Templates/");
        }
    }
}