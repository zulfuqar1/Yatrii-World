using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;

namespace YatriiWorld.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        public BookingController(IMapper mapper, AppDbContext context, IEmailService emailService)
        {
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("create-booking")]
        public async Task<IActionResult> CreateBooking([FromBody] TicketCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tour = await _context.Tours.FindAsync(model.TourId);

            if (tour == null)
            {
                return NotFound(new { isSuccess = false, message = "Error: Selected tour not found in database!" });
            }

            var currentUserEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;

            var ticketEntity = _mapper.Map<Ticket>(model);


            ticketEntity.CheckinDate = tour.StartDate;
            ticketEntity.CheckOutDate = tour.EndDate;
            ticketEntity.TotalPrice = tour.Price * model.TotalPersonCount;
            ticketEntity.PaymentStatus = "Paid";
            ticketEntity.TransactionId = Guid.NewGuid().ToString();
            ticketEntity.PaymentDate = DateTime.Now;

            if (tour.Capacity < model.TotalPersonCount)
            {
                return BadRequest(new
                {
                    isSuccess = false,
                    message = $"Üzgünüz, bu turda sadece {tour.Capacity} kişilik yer kaldı!"
                });
            }

            tour.Capacity -= model.TotalPersonCount;
         
            await _context.Tickets.AddAsync(ticketEntity);
            await _context.SaveChangesAsync();


            try
            {
               
                currentUserEmail = User.FindFirstValue(ClaimTypes.Email)
                                    ?? User.Claims.FirstOrDefault(c => c.Type == "email")?.Value
                                    ?? User.Identity?.Name;



                var targetEmail = ticketEntity.CustomerEmail ?? currentUserEmail;



                if (!string.IsNullOrEmpty(targetEmail))
                {
                    var htmlBody = GetReceiptHtml(ticketEntity, tour, model.TotalPersonCount);
                    await _emailService.SendEmailAsync(targetEmail, "YatriiWorld - Rezervasyon Onayınız", htmlBody);

                }
                else
                {
                    Console.WriteLine(" CRITICAL ERROR ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SMTP EMAIL SENDING ERROR: {ex.Message}");
            }



            return Ok(new
            {
                isSuccess = true,
                message = "Booking created successfully!",
                ticketId = ticketEntity.Id
            });
        }


        private string GetReceiptHtml(Ticket ticket, Tour tour, int totalPersonCount)
        {
       

            return $@"
    <div style='font-family: ""Segoe UI"", Tahoma, Geneva, Verdana, sans-serif; background-color: #f0f8ff; padding: 40px 0;'>
        <div style='max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 12px; overflow: hidden; box-shadow: 0 10px 25px rgba(0,0,0,0.05); border: 1px solid #d1e9ff;'>
            
            <div style='background-color: #00a8e8; padding: 30px; text-align: center;'>
                <h1 style='color: #ffffff; margin: 0; font-size: 28px; letter-spacing: 1px;'>YatriiWorld</h1>
                <p style='color: #e1f5fe; margin: 5px 0 0 0; font-size: 14px; text-transform: uppercase;'>Your adventure begins here.</p>
            </div>

            <div style='padding: 30px;'>
                <h2 style='color: #333; margin-top: 0;'>Your Reservation Confirmation</h2>
                <p style='color: #666; line-height: 1.6;'>
                    Merhaba <strong>{ticket.CustomerFullName}</strong>,<br>
                    Harika bir tercih yaptınız! <strong>{tour.Title ?? tour.Name}</strong> Your tour reservation has been confirmed. You can start preparing now!
                </p>

                <div style='background-color: #f8fdff; border: 1px dashed #00a8e8; border-radius: 8px; padding: 20px; margin: 25px 0;'>
                    <table style='width: 100%; border-collapse: collapse;'>
                        <tr>
                            <td style='padding: 8px 0; color: #555;'><strong>Bilet No:</strong></td>
                            <td style='padding: 8px 0; text-align: right; color: #00a8e8; font-weight: bold;'>YTR-{ticket.Id}-{DateTime.Now.Year}</td>
                        </tr>
                        <tr>
                            <td style='padding: 8px 0; color: #555;'><strong>Check-in:</strong></td>
                            <td style='padding: 8px 0; text-align: right;'>{ticket.CheckinDate:dd MMM yyyy}</td>
                        </tr>
                        <tr>
                            <td style='padding: 8px 0; color: #555;'><strong>Kişi Sayısı:</strong></td>
                            <td style='padding: 8px 0; text-align: right;'>{totalPersonCount} Kişi</td>
                        </tr>
                        <tr>
                            <td style='padding: 20px 0 8px 0; border-top: 1px solid #e1f5fe; font-size: 18px;'><strong>Toplam Tutar</strong></td>
                            <td style='padding: 20px 0 8px 0; border-top: 1px solid #e1f5fe; text-align: right; color: #2ecc71; font-size: 22px; font-weight: bold;'>${ticket.TotalPrice}</td>
                        </tr>
                    </table>
                </div>

                <div style='text-align: center; margin-top: 20px;'>
                    <span style='background-color: #e8f8f0; color: #2ecc71; padding: 8px 16px; border-radius: 50px; font-size: 14px; font-weight: bold;'>
                        ● Ödeme Başarıyla Alındı ({ticket.PaymentStatus})
                    </span>
                </div>
            </div>

            <div style='background-color: #f4faff; padding: 20px; text-align: center; border-top: 1px solid #e1f5fe;'>
         
                <div style='margin-top: 15px;'>
                    <a href='#' style='text-decoration: none; color: #00a8e8; font-size: 12px; margin: 0 10px;'></a>
                    <a href='#' style='text-decoration: none; color: #00a8e8; font-size: 12px; margin: 0 10px;'></a>
                </div>
            </div>
        </div>
    </div>";
        }



    }
}