using System;
using Realms;
using CryptoReminder.Core.CryptoCurrency.RealmContract.Dtos;
using System.Collections.Generic;
using CryptoReminder.Core.CryptoCurrency.Contract.Dtos;
using System.Linq;
using CryptoReminder.Core.Utility;

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
                SchemaVersion = Constants.RealmSchemaVersion
            };
        }

        public CryptoCurrencyReminderDto SaveReminder(CryptoCurrencyReminderDto cryptoCurrencyReminder)
        {
            try
            {
                _realm = Realm.GetInstance(_realmConfiguration);

                _realm.Write(() =>
                    {
                        var alarm = _realm.Find<CryptoCurrencyReminderRealm>(cryptoCurrencyReminder.Id);
                        if (alarm != null)
                        {
                            alarm.LowerLimit = cryptoCurrencyReminder.LowerLimit;
                            alarm.UpperLimit = cryptoCurrencyReminder.UpperLimit;
                            alarm.ExactValue = cryptoCurrencyReminder.ExactValue;
                        }
                        else
                        {
                            alarm = new CryptoCurrencyReminderRealm();

                            var count = _realm.All<CryptoCurrencyReminderRealm>().Count();

                            alarm.Id = count + 1;
                            alarm.MarketName = cryptoCurrencyReminder.MarketName;
                            alarm.LowerLimit = cryptoCurrencyReminder.LowerLimit;
                            alarm.UpperLimit = cryptoCurrencyReminder.UpperLimit;
                            alarm.ExactValue = cryptoCurrencyReminder.ExactValue;

                            cryptoCurrencyReminder.Id = alarm.Id;
                        }
                        _realm.Add(alarm, true);
                    }
                );

            }
            catch(Exception ex)
            {

            }

            return cryptoCurrencyReminder;
        }

        public List<CryptoCurrencyReminderDto> GetReminder(ReminderSearchDto search)
        {
            var reminders = new List<CryptoCurrencyReminderDto>();

            if (search == null)
                return null;
                        
            try
            {
                _realm = Realm.GetInstance(_realmConfiguration);

                switch (search.Type)
                {
                    case SearchType.AllReminders:

                        var allRemindersRealm = _realm.All<CryptoCurrencyReminderRealm>().ToList();
                        foreach (var reminderRealm in allRemindersRealm)
                        {
                            reminders.Add(new CryptoCurrencyReminderDto
                            {
                                Id = reminderRealm.Id,
                                MarketName = reminderRealm.MarketName,
                                LowerLimit = reminderRealm.LowerLimit,
                                UpperLimit = reminderRealm.UpperLimit,
                                ExactValue = reminderRealm.ExactValue,
                            });
                        }
                        break;

                    case SearchType.GroupedReminders:

                        var groupedRemindersRealm = _realm.All<CryptoCurrencyReminderRealm>().ToList();
                        var groupedReminders = groupedRemindersRealm.GroupBy(x => x.MarketName).Select(x => x.FirstOrDefault());
                        foreach (var reminderRealm in groupedReminders)
                        {
                            reminders.Add(new CryptoCurrencyReminderDto
                            {
                                Id = reminderRealm.Id,
                                MarketName = reminderRealm.MarketName,
                                LowerLimit = reminderRealm.LowerLimit,
                                UpperLimit = reminderRealm.UpperLimit,
                                ExactValue = reminderRealm.ExactValue,
                            });
                        }
                        break;

                    case SearchType.AllRemindersFor1Coin:

                        var cryptoCurrency = search.cryptoCurrency;
                        if (cryptoCurrency != null)
                        {
                            var realmReminders = _realm.All<CryptoCurrencyReminderRealm>().ToList();
                            var realmRemindersList = realmReminders.Where(x => x.MarketName == cryptoCurrency.MarketName);
                            foreach (var reminderRealm in realmRemindersList)
                            {
                                reminders.Add(new CryptoCurrencyReminderDto
                                {
                                    Id = reminderRealm.Id,
                                    MarketName = reminderRealm.MarketName,
                                    LowerLimit = reminderRealm.LowerLimit,
                                    UpperLimit = reminderRealm.UpperLimit,
                                    ExactValue = reminderRealm.ExactValue,
                                });
                            }
                        }
                        break;
                }
            }
            catch(Exception ex)
            {

            }

            return reminders;
        }

        public CryptoCurrencyReminderDto GetReminderDetails(CryptoCurrencyReminderDto reminder)
        {
            CryptoCurrencyReminderDto cryptoCurrencyReminder = null;

            try
            {
                _realm = Realm.GetInstance(_realmConfiguration);

                var reminderRealm = _realm.Find<CryptoCurrencyReminderRealm>(reminder.Id);

                if (reminderRealm != null)
                {
                    cryptoCurrencyReminder = new CryptoCurrencyReminderDto
                    {
                        Id = reminderRealm.Id,
                        MarketName = reminderRealm.MarketName,
                        LowerLimit = reminderRealm.LowerLimit,
                        UpperLimit = reminderRealm.UpperLimit,
                        ExactValue = reminderRealm.ExactValue,
                    };
                }
            }
            catch (Exception ex)
            {

            }

            return cryptoCurrencyReminder;
        }

        public bool RemoveReminder(CryptoCurrencyReminderDto cryptoCurrencyReminder)
        {
            try
            {
                _realm = Realm.GetInstance(_realmConfiguration);

                var reminderRealm = _realm.Find<CryptoCurrencyReminderRealm>(cryptoCurrencyReminder.Id);
                if (reminderRealm != null)
                {
                    _realm.Write(() =>
                    {
                        _realm.Remove(reminderRealm);
                    });
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
