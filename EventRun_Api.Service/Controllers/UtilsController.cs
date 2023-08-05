﻿using Aspose.Cells;
using EventRun_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net.Mail;
using System.Text;
using EventRun_Api.Core;
using EventRun_Api.Models.Models;
using EventRun_Api.Models.Enums;

namespace EventRun_Api.Service.Controllers
{
    public class UtilsController
    {
        private readonly IConfiguration _config;
        //private readonly InscriptionDataCore _inscriptionDataCore = new();

        public UtilsController(IConfiguration config) { _config = config; }

        public void SendEmail(InscriptionDataResponse inscriptionData, RunnerResponse runner) {
            #region config email service
            string emailFrom = _config.GetValue<string>("AppSettings:EmailFrom");
            string emailDisplayName = _config.GetValue<string>("AppSettings:EmailDisplayName");
            string emailSubject = _config.GetValue<string>("AppSettings:EmailSubject");
            string emailHost = _config.GetValue<string>("AppSettings:EmailHost");
            int emailPort = _config.GetValue<int>("AppSettings:EmailPort");
            string emailPassword = _config.GetValue<string>("AppSettings:EmailPassword");
            bool emailEnableSsl = _config.GetValue<bool>("AppSettings:EmailEnableSsl");

            SmtpClient smtp = new()
            {
                UseDefaultCredentials = false,
                Host = emailHost,
                Port = emailPort,
                Credentials = new System.Net.NetworkCredential(emailFrom, emailPassword),
                EnableSsl = emailEnableSsl
            };

            #endregion

            #region config email
            MailMessage email = new()
            {
                From = new MailAddress(emailFrom, emailDisplayName, System.Text.Encoding.UTF8),
                Subject = emailSubject,
                Body = GetBodyEmail(inscriptionData, runner),
                IsBodyHtml = true,
                Priority = MailPriority.Normal,
            };
            email.To.Add(runner.Email);
            #endregion
            try
            {
                smtp.Send(email);
            }
            catch (Exception)
            {
                throw;
            }
        }


        private string GetBodyEmail(InscriptionDataResponse inscriptionData, RunnerResponse runner) {
            string body = GetStringOfFile(_config.GetValue<string>("AppSettings:TemplatePath"));
            return body.Replace("@inscription.registrationDate", inscriptionData.RegistrationDate.ToString("dd/MM/yyyy"))
                .Replace("@runner.documentType",runner.DocumentType)
                .Replace("@runner.documentNumber",runner.DocumentNumber.ToString())
                .Replace("@runner.firstName",runner.FirstName)
                .Replace("@runner.lastName",runner.LastName)
                .Replace("@runner.gender",runner.Gender)
                .Replace("@runner.birthDate",runner.BirthDate.ToString("dd/MM/yyyy"))
                .Replace("@runner.bloodType",runner.BloodType)
                .Replace("@runner.email",runner.Email)
                .Replace("@runner.phone",runner.Phone)
                .Replace("@runner.address",runner.Address)
                .Replace("@runner.city",runner.City)
                .Replace("@runner.countryNationality",runner.CountryNationality)
                .Replace("@inscription.airlineCityOrigin", inscriptionData.AirlineCityOrigin)
                .Replace("@inscription.departureDate", inscriptionData.DepartureDate?.ToString("dd/MM/yyyy"))
                .Replace("@inscription.returnDate", inscriptionData.ReturnDate?.ToString("dd/MM/yyyy"))
                .Replace("@inscription.paymentMethod", inscriptionData.PaymentMethod)
                .Replace("@inscription.proofPayment", inscriptionData.ProofPayment)
                .Replace("@inscription.detailsPayment", inscriptionData.DetailsPayment)
                .Replace("@inscription.race", inscriptionData.Race)
                .Replace("@inscription.category", inscriptionData.Category)
                .Replace("@inscription.tshirtSize", inscriptionData.TshirtSize)
                .Replace("@inscription.authorizationListEnrolled", inscriptionData.AuthorizationListEnrolled == true ? "Si" : "No" )
                .Replace("@inscription.club", inscriptionData.Club)
                .Replace("@inscription.observations", inscriptionData.Observations)
                .Replace("@inscription.acceptanceTyC", inscriptionData.AcceptanceTyC == true ? "Si" : "No")
                .Replace("@runner.emergencyContactName", runner.EmergencyContactName)
                .Replace("@runner.emergencyContactPhone", runner.EmergencyContactPhone);
        }

        public string GetStringOfFile(string templatePath)
        {
            try
            {
                FileStream fileStream = System.IO.File.OpenRead(templatePath);
                using var sr = new StreamReader(fileStream, Encoding.UTF8);
                return sr.ReadToEnd();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
