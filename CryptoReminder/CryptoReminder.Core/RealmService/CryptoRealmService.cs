using System;
using Realms;
using CryptoReminder.Core.CryptoCurrency.RealmContract.Dtos;
using System.Collections.Generic;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;

namespace CryptoReminder.Core.RealmService
{
    public class CryptoRealmService : ICryptoRealmService
    {
        private RealmConfiguration _realmConfiguration;
        private Realm _realm;

        public CryptoRealmService()
        {
            _realmConfiguration = new RealmConfiguration
            {
                SchemaVersion = 1
            };

        }

        public void SaveReminder(CryptoCurrencyReminderDto cryptoCurrencyReminder)
        {
            try
            {
                _realm = Realm.GetInstance(_realmConfiguration);

                _realm.Write(() =>
                    {
                        var alarm = _realm.Find<CryptoCurrencyReminderRealm>(cryptoCurrencyReminder.MarketName);
                        if (alarm != null)
                        {
                            alarm.IsAlarmSet = cryptoCurrencyReminder.IsAlarmSet;
                            alarm.Last = cryptoCurrencyReminder.Last;
                        }
                        else
                        {
                            alarm = new CryptoCurrencyReminderRealm();
                            alarm.MarketName = cryptoCurrencyReminder.MarketName;
                            alarm.IsAlarmSet = cryptoCurrencyReminder.IsAlarmSet;
                            alarm.Last = cryptoCurrencyReminder.Last;
                        }
                        _realm.Add(alarm, true);

                    }
                );

            }
            catch(Exception ex)
            {

            }
            
        }

        public List<CryptoCurrencyReminderDto> GetReminders()
        {
            var reminders = new List<CryptoCurrencyReminderDto>();

            try
            {
                _realm = Realm.GetInstance(_realmConfiguration);

                var remindersRealm = _realm.All<CryptoCurrencyReminderRealm>();

                foreach(var reminderRealm in remindersRealm)
                {
                    reminders.Add(new CryptoCurrencyReminderDto
                    {
                        MarketName = reminderRealm.MarketName,
                        IsAlarmSet = reminderRealm.IsAlarmSet,
                        Last = reminderRealm.Last
                    });
                }
            }
            catch(Exception ex)
            {

            }

            return reminders;
        }

        public bool CheckAlarmStatus(CryptoCurrencyDto cryptoCurrency)
        {
            try
            {
                _realm = Realm.GetInstance(_realmConfiguration);

                var remiderRealm = _realm.Find<CryptoCurrencyReminderRealm>(cryptoCurrency.MarketName);

                if (remiderRealm != null && remiderRealm.IsAlarmSet)
                {
                    return true;
                }
            }
            catch(Exception ex)
            {

            }

            return false;
        }

        public CryptoCurrencyReminderDto GetRemider(CryptoCurrencyDto cryptoCurrency)
        {
            CryptoCurrencyReminderDto cryptoCurrencyReminder = null;

            try
            {
                _realm = Realm.GetInstance(_realmConfiguration);

                var remiderRealm = _realm.Find<CryptoCurrencyReminderRealm>(cryptoCurrency.MarketName);

                if (remiderRealm != null && remiderRealm.IsAlarmSet)
                {
                    cryptoCurrencyReminder = new CryptoCurrencyReminderDto
                    {
                        MarketName = remiderRealm.MarketName,
                        IsAlarmSet = remiderRealm.IsAlarmSet,
                        Last = remiderRealm.Last
                    };
                }
            }
            catch (Exception ex)
            {

            }

            return cryptoCurrencyReminder;
        }
    }
}
