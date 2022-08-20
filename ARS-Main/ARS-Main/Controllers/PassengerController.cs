using ARS_Main.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;


namespace ARS_Main.Controllers
{
    [Authorize]
    public class PassengersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Passenger Profile Funtion -> that return the suitable view 
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();

            //View Informations, View Tickets Details in table   
            PassengerUserTicketView PUTV = new PassengerUserTicketView
            {
                Passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.ApplicationUser.Id.Equals(UserId)),
                Tickets = db.Tickets.Include(c => c.Flight).ToList(),
                Flights = db.Flights.ToList(),
                Planes = db.Planes.ToList()
            };
            if (PUTV.Passenger == null)
            {
                ApplicationUser user = db.Users.SingleOrDefault(c => c.Id == UserId);
                if (user != null)
                {
                    return View(user);
                }
                return HttpNotFound();
            }
            return View("PassengerProfile", PUTV);
        }

        public ActionResult Booking(string fid)
        {
            int ids = int.Parse(fid);
            BookingViewModel BVM = new BookingViewModel
            {
                Flights = db.Flights.Where(l => l.ID == ids).Include(c => c.Plane).Include(c => c.Plane.BookedSeats).ToList()
            };
            //To return unexpired Flight
            var Flights = new List<Flight>();
            foreach (var Flight in BVM.Flights)
            {
                DateTime Time = DateTime.Parse(Flight.DepDate);
                String TimeString = Time.ToString("dd/MM/yyyy HH:mm");
                Time = DateTime.ParseExact(TimeString, "dd/MM/yyyy HH:mm", null);
                DateTime Today = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), "dd/MM/yyyy HH:mm", null);
                if (DateTime.Compare(Time, Today) >= 0)
                {
                    Flights.Add(Flight);
                }
            }
            BVM.Flights = Flights;

            return View(BVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Booking(BookingTicket ticket)
        {
            if (ModelState != null)
            {
                Flight flight = db.Flights.Include(c => c.Plane).Include(c => c.Plane.BookedSeats).SingleOrDefault(c => c.ID == ticket.Flight);
                //Plane plane = db.Planes.SingleOrDefault(c => c.PlaneID == flight.PlaneId);
                if (flight != null)
                {
                    var FreeSeats = flight.Plane.SeatingCapacity;
                    if (flight.Plane.BookedSeats.Count != 0)
                    {
                        FreeSeats = flight.Plane.SeatingCapacity - flight.Plane.BookedSeats.Count;
                    }

                    if (FreeSeats >= ticket.NumberOfChairs)
                    {
                        var UserId = User.Identity.GetUserId();
                        ApplicationUser user = db.Users.Single(c => c.Id.Equals(UserId));
                        if (user != null)
                        {
                            Passenger passenger = db.Passengers.SingleOrDefault(c => c.ApplicationUser.Id == user.Id);
                            if (passenger == null)
                            {
                                //Creating Passenger and Adding it to DB
                                passenger = new Passenger();
                                passenger.ApplicationUser = user;
                                passenger.Tickets = new List<Models.Ticket>();

                                db.Passengers.Add(passenger);
                                db.SaveChanges();
                            }
                            else if (passenger != null && passenger.Blocked == false)
                            {
                                if (passenger.Tickets == null)
                                {
                                    passenger.Tickets = new List<Models.Ticket>();
                                }

                                var BookedSeats = flight.Plane.BookedSeats;
                                List<int> SeatsNumbers = new List<int>();
                                foreach (var Seat in BookedSeats)
                                {
                                    if (Seat.IsAvailable == true)
                                        SeatsNumbers.Add(Seat.SeatNumber);
                                }
                                for (int i = 0; i < ticket.NumberOfChairs; i++)
                                {
                                    for (int y = 0; y < flight.Plane.SeatingCapacity; y++)
                                    {
                                        if (!SeatsNumbers.Contains(y))
                                        {
                                            //Initializate new Seat
                                            Seat one = new Seat
                                            {
                                                IsAvailable = false,
                                                PassengerId = passenger.Id,
                                                SeatNumber = y + 1
                                            };
                                            //Save Seat in DB and add it in BookedSeats in Bus of the particilar Trip
                                            //Seat SeatId = db.Seats.Add(one);
                                            flight.Plane.BookedSeats.Add(one);
                                            SeatsNumbers.Add(y);
                                            break;
                                        }
                                    }
                                }

                                Ticket Newticket = new Ticket
                                {
                                    Flight = flight,
                                    BookingTime = DateTime.Now.ToString(),
                                    IsBlocked = false
                                };

                                passenger.Tickets.Add(Newticket);
                                //Save new Seats to DB
                                db.SaveChanges();

                                try
                                {
                                    MailMessage mail = new MailMessage();
                                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                                    mail.From = new MailAddress("m.fadel3597@gmail.com");
                                    mail.To.Add(User.Identity.GetUserName());
                                    mail.Subject = "Booking Success Test";

                                    SmtpServer.Port = 587;
                                    SmtpServer.UseDefaultCredentials = true;
                                    SmtpServer.Credentials = new System.Net.NetworkCredential("m.fadel3597@gmail.com", "3597@m.Fade!#K*K0");
                                    SmtpServer.EnableSsl = true;
                                    SmtpServer.Send(mail);

                                }
                                catch
                                {
                                    return RedirectToAction("Index");//After Booking
                                }

                            }
                        }
                        else
                        {
                            //User Not Found
                            return HttpNotFound();
                        }
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    //Trip Not Found
                    return HttpNotFound();
                }
                return RedirectToAction("Index");//After Booking
            }
            return RedirectToAction("Booking");
        }

        [Route("Passengers/CancelBooking/{pid}/{tid}")]
        public ActionResult CancelBooking(int pid, int tid)
        {
            if (pid == 0 || tid == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PassengerTicketView PTV = new PassengerTicketView
            {
                Passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == pid),
                Ticket = db.Tickets.Include(c => c.Flight).Include(c => c.Flight.Plane).Include(c => c.Flight.Plane.BookedSeats).Single(c => c.Id == tid),
                Seats = db.Seats.Include(c => c.Passenger).Where(c => c.PassengerId == pid)
            };

            if (PTV.Passenger == null || PTV.Ticket == null || PTV.Seats == null)
            {
                return HttpNotFound();
            }
            //Delete All Seats Before Ticket 
            foreach (var Seat in PTV.Seats)
            {
                if (PTV.Ticket.Flight.Plane.BookedSeats.Contains(Seat))
                {
                    db.Seats.Remove(Seat);
                }
            }

            if (PTV.Passenger.Tickets.Count == 1)
            {
                //34an lw mfe4 8er el ticket de w at3mlha Delete yrg3 tany User Not Passenger
                db.Tickets.Remove(PTV.Ticket);
                db.Passengers.Remove(PTV.Passenger);
            }
            else
            {
                db.Tickets.Remove(PTV.Ticket);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [Route("Passengers/Ticket/{pid}/{tid}/{trip}")]
        public ActionResult Ticket(int pid, int tid, int trip)
        {
            //W ynf3 y3dl el feedback aw y3mlo Delete
            //Hyb2a Fe 3 Bottuns Add/Edit/Delete FeedBack, Add Seat and Delete if it not expired
            //Hyb2a Fe 3 Bottuns Add/Edit/Delete FeedBack if it expired
            if (pid == 0 || tid == 0 || trip == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerTicketView PUTV = new PassengerTicketView
            {
                Passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == pid),
                Ticket = db.Tickets.Include(c => c.Flight).Include(c => c.Flight.Plane).SingleOrDefault(c => c.Id == tid),
                Seats = db.Seats.Include(cc => cc.Passenger).Where(cc => cc.PassengerId == pid),
            };

            if (PUTV.Passenger == null || PUTV.Ticket == null)
            {
                return HttpNotFound();
            }

            return View(PUTV);
        }

        //Will Go to Add Feedback form
        /*[Route("Passengers/Feedback/{pid}/{trip}")]
        [HttpGet]
        public ActionResult Feedback(int pid, int trip)
        {
            if (pid == 0 || trip == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //hna hb3t feedback model feh el pid w el tripid 34an ast5dmhom 3la el button hnak 
            //aw mmkn ab3t el passenger w trip nfsho 34an atl3lo details fo2 el feedback message area
            Feedback feedback = new Feedback
            {
                PassengerId = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == pid),
                TripId = db.Trips.Include(c => c.Line).Include(c => c.Bus).Include(c => c.Bus.Driver).Single(c => c.Id == trip)
            };
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Passengers/Feedback/{pid}/{trip}")]
        public ActionResult Feedback(Feedback feedback)
        {

            var UserId = User.Identity.GetUserId();
            Passenger SessionPassenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.ApplicationUser.Id.Equals(UserId));
            Passenger passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == feedback.PassengerId.Id);
            Trip trip = db.Trips.Single(c => c.Id == feedback.TripId.Id);
            feedback.PassengerId = passenger;

            feedback.TripId = trip;

            if (SessionPassenger != passenger)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (feedback.FeedbackMessage != "")
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }*/


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