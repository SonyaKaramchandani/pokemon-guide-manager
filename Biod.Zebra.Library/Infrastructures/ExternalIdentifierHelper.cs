using Biod.Zebra.Library.EntityModels.Zebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biod.Zebra.Library.Infrastructures
{
    public static class ExternalIdentifierHelper
    {
        public static void RegisterExternalIdentifier(BiodZebraEntities dbContext, string externalName, string externalId, string userId)
        {
            var existingRecord = dbContext.UserExternalIds.FirstOrDefault(e => e.ExternalName == externalName && e.ExternalId == externalId);
            if (existingRecord == null)
            {
                // New Identifier being registered
                dbContext.UserExternalIds.Add(new UserExternalId()
                {
                    ExternalId = externalId,
                    ExternalName = externalName,
                    UserId = userId,
                    LastCommunicationDate = DateTimeOffset.UtcNow
                });
                dbContext.SaveChanges();
                return;
            }

            // Update existing record with latest data
            existingRecord.UserId = userId;
            existingRecord.LastCommunicationDate = DateTimeOffset.UtcNow;
            dbContext.SaveChanges();
        }

        public static List<string> GetExternalIdentifier(BiodZebraEntities dbContext, string externalName, string userId)
        {
            return dbContext.UserExternalIds
                .Where(e => e.ExternalName == externalName && e.UserId == userId)
                .Select(r => r.ExternalId)
                .ToList();
        }
    }
}
