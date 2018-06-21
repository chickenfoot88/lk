using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using OhDataManager.Enums;
using WebAppAuth.Context;
using WebAppAuth.Entities;
using WebAppAuth.Models;
using WebAppAuth.Services.Data;
using WebAppAuth.WebApi.Models.Requests;

namespace WebAppAuth.WebApi.Controllers
{
    /// <summary>
    /// Лицевой счет
    /// </summary>
    public class PersonalAccountController : BaseApiController
    {
        // POST: api/PersonalAccount/FindPersonalAccount
        public async Task<IHttpActionResult> FindPersonalAccount(PersonalAccountRequest personalAccountRequest)
        {
            var personalAccountResult = await OhConfigurator.Container.Resolve<IAbonentService>()
                .GetPersonalAccount(personalAccountRequest.PaymentCode,
                    personalAccountRequest.ApartmentOwnerFullName, personalAccountRequest.ApartmentNumber);

            if (!personalAccountResult.Success)
                return NotFound();

            return Ok(new
            {
                personalAccountResult.Content.Id,
                personalAccountResult.Content.ApartmentOwnerFullName,
                personalAccountResult.Content.ApartmentFullAddress,
                personalAccountResult.Content.ManagmentOrganizationName
            });
        }


        // POST: api/PersonalAccount/Accruals
        public async Task<IHttpActionResult> Accruals(CommonRequest request)
        {
            var accruals = await OhConfigurator.Container.Resolve<IAbonentService>()
                .GetAccruals(request.Id, request.Date);

            if (!accruals.Success)
                return NotFound();


            return Ok(new
            {
                Accruals = accruals.Content.Select(x => new
                {
                    x.PersonalAccountId,
                    x.CalculationDate,
                    CalculationDateString = x.CalculationDate.ToString("d MMM yyyy"),
                    CalculationMonth = x.CalculationDate.Month,
                    CalculationYear = x.CalculationDate.Year,
                    x.ServiceName,
                    x.MeasureName,
                    x.SupplierName,
                    x.SumPayd,
                    x.Tariff,
                    x.AccruedForPayment,
                    x.AccruedTariffNondelivery,
                    x.NondeliveryDaysCount,
                    x.SumInsaldo,
                    DebtOrOverpayment = x.SumInsaldo - x.SumPayd,
                    x.ServiceId
                }),
                Totals =
                    new
                    {
                        DebtOrOverpayment =
                            accruals.Content.Sum(x => x.SumInsaldo) - accruals.Content.Sum(x => x.SumPayd),
                        SumPayd = accruals.Content.Sum(x => x.SumPayd),
                        AccruedForPayment = accruals.Content.Sum(x => x.AccruedForPayment),
                        SumInsaldo = accruals.Content.Sum(x => x.SumInsaldo),
                        AccruedTariffNondelivery = accruals.Content.Sum(x => x.AccruedTariffNondelivery)
                    }
            });
        }


        // POST: api/PersonalAccount/Payments
        public async Task<IHttpActionResult> Payments(CommonRequest request)
        {
            var payments = await OhConfigurator.Container.Resolve<IAbonentService>()
                .GetPayments(request.Id, request.PeriodBegin, request.PeriodEnd);

            if (!payments.Success)
                return NotFound();

            return Ok(new
            {
                Payments = payments.Content.Select(x => new
                {
                    x.PersonalAccountId,

                    x.CalculationDate,
                    CalculationDateString = x.CalculationDate.ToString("d MMM yyyy"),
                    CalculationMonth = x.CalculationDate.Month,
                    CalculationYear = x.CalculationDate.Year,

                    x.PaymentDate,
                    PaymentDateString = x.PaymentDate.ToString("d MMM yyyy"),
                    PaymentMonth = x.PaymentDate.Month,
                    PaymentYear = x.PaymentDate.Year,

                    PaydCalculationDate = x.PaydCalculationMonth,
                    PaydCalculationDateString = x.PaydCalculationMonth.ToString("d MMM yyyy"),
                    PaydCalculationMonth = x.PaydCalculationMonth.Month,
                    PaydCalculationYear = x.PaydCalculationMonth.Year,

                    x.PaymentPlacement,
                    x.SumPayment
                }),
                Totals =
                    new
                    {
                        SumPayment = payments.Content.Sum(x => x.SumPayment)
                    }
            });
        }

        // POST: api/PersonalAccount/MeterReadings
        public async Task<IHttpActionResult> MeterReadings(CommonRequest request)
        {
            var meterReadings = await OhConfigurator.Container.Resolve<IAbonentService>()
                .GetMeterReadings(request.Id, request.PeriodBegin, request.PeriodEnd);

            if (!meterReadings.Success)
                return NotFound();

            return Ok(new
            {
                MeterReadings = meterReadings.Content.Select(x => new
                {
                    x.PersonalAccountId,
                    MeterReadingId = x.Id,

                    x.CalculationDate,
                    CalculationDateString = x.CalculationDate.ToString("d MMM yyyy"),
                    CalculationMonth = x.CalculationDate.Month,
                    CalculationYear = x.CalculationDate.Year,

                    ServiceId = x.MeterServiceId,
                    x.ServiceName,
                    x.MeasureName,
                    x.MeterDeviceNumber,
                    x.Capacity,
                    x.EnteredValue,
                    x.Indication,

                    x.CalculationApplyingDate,
                    CalculationApplyingDateString = x.CalculationApplyingDate.ToString("d MMM yyyy"),
                    CalculationApplyingMonth = x.CalculationApplyingDate.Month,
                    CalculationApplyingYear = x.CalculationApplyingDate.Year,

                    x.IndicationPrevious,
                    x.CalculationApplyingDatePrevious,
                    CalculationApplyingDatePreviousString = x.CalculationApplyingDatePrevious?.ToString("d MMM yyyy"),
                    CalculationApplyingDatePreviousMonth = x.CalculationApplyingDatePrevious?.Month,
                    CalculationApplyingDatePreviousYear = x.CalculationApplyingDatePrevious?.Year
                })
            });
        }

        // POST: api/PersonalAccount/LastMeterReadings
        public async Task<IHttpActionResult> LastMeterReadings(CommonRequest request)
        {
            var meterReadings = await OhConfigurator.Container.Resolve<IAbonentService>()
                .GetLastMeterReadings(request.Id);

            if (!meterReadings.Success)
                return NotFound();

            return Ok(new
            {
                MeterReadings = meterReadings.Content.Select(x => new
                {
                    x.PersonalAccountId,
                    MeterReadingId = x.Id,

                    x.CalculationDate,
                    CalculationDateString = x.CalculationDate.ToString("d MMM yyyy"),
                    CalculationMonth = x.CalculationDate.Month,
                    CalculationYear = x.CalculationDate.Year,

                    ServiceId = x.MeterServiceId,
                    x.ServiceName,
                    x.MeasureName,
                    x.MeterDeviceNumber,
                    x.Capacity,
                    x.EnteredValue,
                    x.Indication,

                    x.CalculationApplyingDate,
                    CalculationApplyingDateString = x.CalculationApplyingDate.ToString("d MMM yyyy"),
                    CalculationApplyingMonth = x.CalculationApplyingDate.Month,
                    CalculationApplyingYear = x.CalculationApplyingDate.Year,

                    x.IndicationPrevious,
                    x.CalculationApplyingDatePrevious,
                    CalculationApplyingDatePreviousString = x.CalculationApplyingDatePrevious?.ToString("d MMM yyyy"),
                    CalculationApplyingDatePreviousMonth = x.CalculationApplyingDatePrevious?.Month,
                    CalculationApplyingDatePreviousYear = x.CalculationApplyingDatePrevious?.Year
                }),
                EnterAllowed = true
            });
        }

        // POST: api/PersonalAccount/MeterReadings
        public async Task<IHttpActionResult> EnterMeterReading(CommonRequest request)
        {
            var result = await OhConfigurator.Container.Resolve<IAbonentService>()
                .EnterMeterReading(request.Id, request.Value, EInformationSource.MobileApp);

            return Ok(new
            {
                result.InfoMessage,
                result.Success
            });
        }

        // POST: api/PersonalAccount/SocialPayments
        public async Task<IHttpActionResult> SocialPayments(CommonRequest request)
        {
            var socialPayments = await OhConfigurator.Container.Resolve<IAbonentService>()
                .GetSocialPayments(request.Id, request.PeriodBegin, request.PeriodEnd);

            if (!socialPayments.Success)
                return NotFound();

            return Ok(new
            {
                SocialPayments = socialPayments.Content.Select(x => new
                {
                    x.PersonalAccountId,
                    x.CalculationDate,
                    CalculationDateString = x.CalculationDate.ToString("d MMM yyyy"),
                    CalculationMonth = x.CalculationDate.Month,
                    CalculationYear = x.CalculationDate.Year,
                    x.ArticleName,
                    x.ArticleGroupName,
                    x.PaymentPlacement,
                    x.SumInsaldo,
                    x.SumDelta,
                    x.SumAccrued,
                    x.SumRecalculation,
                    x.SumPayoff
                }),
                Totals =
                    new
                    {
                        SumInsaldo = socialPayments.Content.Sum(x => x.SumInsaldo),
                        SumDelta = socialPayments.Content.Sum(x => x.SumDelta),
                        SumAccrued = socialPayments.Content.Sum(x => x.SumAccrued),
                        SumRecalculation = socialPayments.Content.Sum(x => x.SumRecalculation),
                        SumPayoff = socialPayments.Content.Sum(x => x.SumPayoff)
                    }
            });
        }

        // POST: api/PersonalAccount/ClaimsDictionary
        [Authorize]
        public async Task<IHttpActionResult> ClaimsDictionary()
        {
            using (var context = new ApplicationDbContext())
            {
                var dict = await context.DictClaimKind.ToListAsync();
                return Ok(dict
                    .Select(x =>
                        new
                        {
                            x.Id,
                            x.Name
                        })
                    );
            }
        }


        // POST: api/PersonalAccount/Claims
        [Authorize]
        public async Task<IHttpActionResult> Claims()
        {
            var userIdentityid = User.Identity.GetUserId<int>();
            using (var context = new ApplicationDbContext())
            {
                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return NotFound();
                if (abonents.Count() > 1) return BadRequest();

                var abonent = abonents.Single();

                var claims = (await OhConfigurator.Container.Resolve<IAbonentService>()
                    .GetClaims(abonent.PersonalAccountId));

                if (!claims.Success)
                    return NotFound();

                return Ok(new
                {
                    Claims = claims.Content
                        .OrderByDescending(x => x.CreationTime)
                        .Select(x => new
                        {
                            x.Id,
                            x.PersonalAccountId,
                            ClaimStatusId = x.ClaimStatus,
                            x.ClaimStatusText,
                            x.CompleteDate,
                            x.Description,
                            ClaimKindId = x.DictClaimKindId,
                            ClaimKindName = x.DictClaimKind.Name,
                            ClaimKindNote = x.DictClaimKind.Note,
                            EmployeeName = x.Employee?.DisplayName,
                            x.CreationTime,
                            CreationTimeString = x.CreationTime.ToString("d MMM yyyy"),
                            CreationTimeMonth = x.CreationTime.Month,
                            CreationTimeYear = x.CreationTime.Year,
                            x.ChangedTime,
                            ChangedTimeString = x.CreationTime.ToString("d MMM yyyy"),
                            ChangedTimeMonth = x.CreationTime.Month,
                            ChangedTimeYear = x.CreationTime.Year,
                        })
                });
            }
        }

        // POST: api/PersonalAccount/CreateClaim
        [Authorize]
        public async Task<IHttpActionResult> CreateClaim(ClaimViewModel claimViewModel)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var userIdentityid = User.Identity.GetUserId<int>();

                    var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                    if (!abonents.Any()) return NotFound();
                    if (abonents.Count() > 1) return BadRequest();

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

                    context.AbonentClaim.Add(claim);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
        }


        // POST: api/PersonalAccount/ClaimDetails/{id}
        [Authorize]
        public async Task<IHttpActionResult> ClaimDetails(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();

                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return NotFound();
                if (abonents.Count() > 1) return BadRequest();

                var abonent = abonents.Single();

                //отображаем заявки созданные только для ЛС абонента
                var claim = await context.AbonentClaim
                    .Include(x => x.Employee)
                    .Include(x => x.DictClaimKind)
                    .FirstOrDefaultAsync(x => x.Id == id
                                              && x.PersonalAccountId == abonent.PersonalAccountId);

                if (claim == null)
                {
                    return NotFound();
                }

                //список комментариев к заявке
                var comments = await context.AbonentClaimComment
                    .Where(x => x.AbonentClaimId == claim.Id)
                    .Include(x => x.AbonentClaim)
                    .Include(x => x.AbonentClaim.Creator)
                    .Include(x => x.Employee)
                    .Select(x => new AbonentClaimCommentViewModel
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        Creator = x.AbonentClaim.Creator,
                        Employee = x.Employee,
                        CreationTime = x.CreationTime
                    })
                    .ToListAsync();

                return Ok(new ClaimViewModel
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
                    ImportedFileId = claim.ImportedFileId
                });
            }
        }

        // POST: api/PersonalAccount/CommentAbonentClaim
        [Authorize]
        public async Task<IHttpActionResult> CommentAbonentClaim(AbonentClaimCommentRequest model)
        {
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();

                var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!abonents.Any()) return NotFound();
                if (abonents.Count() > 1) return BadRequest();

                var abonent = abonents.Single();

                var claimComment = new AbonentClaimComment
                {
                    OrganizationId = abonent.OrganizationId,
                    AbonentClaimId = model.AbonentClaimId,
                    Comment = model.Comment,
                    ChangedTime = DateTime.Now,
                    CreationTime = DateTime.Now,
                    InformationSource = EInformationSource.Abonent
                };

                context.AbonentClaimComment.Add(claimComment);
                await context.SaveChangesAsync();
                return Ok();
            }
        }

        // POST: api/PersonalAccount/CloseClaim
        [Authorize]
        public async Task<IHttpActionResult> CloseAbonentClaim(AbonentClaimCloseRequest model)
        {
            if (model.AbonentClaimId == default(long))
                return BadRequest("Не задан идентификатор");

            using (var context = new ApplicationDbContext())
            {
                var claim = await context.AbonentClaim.FindAsync(model.AbonentClaimId);
                if (claim == null)
                    return NotFound();

                var userIdentityid = User.Identity.GetUserId<int>();

                    var abonents = context.Abonent.Where(x => x.ApplicationUser.Id == userIdentityid);

                    if (!abonents.Any()) return NotFound();
                    if (abonents.Count() > 1) return BadRequest();

                    var abonent = abonents.Single();


                if (claim.CreatorId != abonent.Id)
                    return NotFound();

                claim.Rating = model.Rating;
                claim.Accepted = true;
                claim.CompleteDate = DateTime.Now;
                claim.ClaimStatus = EClaimStatus.Closed;

                await context.SaveChangesAsync();


                var claimComment = new AbonentClaimComment
                {
                    OrganizationId = abonent.OrganizationId,
                    AbonentClaimId = model.AbonentClaimId,
                    Comment = model.Comment,
                    ChangedTime = DateTime.Now,
                    CreationTime = DateTime.Now,
                    InformationSource = EInformationSource.Abonent
                };

                context.AbonentClaimComment.Add(claimComment);

                await context.SaveChangesAsync();

                return Ok();
            }
        }
    }
}