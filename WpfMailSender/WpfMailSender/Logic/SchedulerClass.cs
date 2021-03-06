﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using EmailSendService;

namespace WpfMailSender.Logic
{
    /// <summary>
    /// Планировщик
    /// </summary>
    class SchedulerClass
    {
        #region Поля

        /// <summary>
        /// Таймер
        /// </summary>
        private DispatcherTimer _timer = new DispatcherTimer();

        /// <summary>
        /// Экземпляр класса, отвечающего за отправку писем
        /// </summary>
        private EmailSendServiceClass _emailSender;

        /// <summary>
        /// Дата и время отправки
        /// </summary>
        private DateTime _dtSend;

        /// <summary>
        /// Коллекция адресатов
        /// </summary>
        private IQueryable<Email> _emails;

        #endregion

        /// <summary>
        /// Отложенная рассылка писем
        /// </summary>
        /// <param name="dtSend">Дата отправки</param>
        /// <param name="emailSender">Рассыльщик</param>
        /// <param name="emails">Адреса получателей</param>
        /// <param name="mailSender">Объект основной формы для обработки заврешения рассылки из другого потока</param>
        public void SendEmails(DateTime dtSend, EmailSendServiceClass emailSender, IQueryable<Email> emails, MailSender mailSender)
        {
            _emailSender = emailSender;
            emailSender.ShowResultOfSend += mailSender.SendEndWindowCreate;
            _dtSend = dtSend;
            _emails = emails;
            _timer.Tick += TimerOnTick;
            _timer.Interval = new TimeSpan(0,0,1);
            _timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (_dtSend.ToShortTimeString() == DateTime.Now.ToShortTimeString())
            {
                new Thread(() =>
                {
                    Dictionary<string, string> emails = _emails.ToDictionary(k => k.Email1, p => p.Name);
                    _emailSender.SendMail(emails);
                    _timer.Stop();
                }).Start();
            }
        }
    }
}
