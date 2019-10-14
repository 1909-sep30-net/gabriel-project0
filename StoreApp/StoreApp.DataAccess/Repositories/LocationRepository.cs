using StoreApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DataAccess.Repositories
{
    public class LocationRepository
    {
        private DoapSoapContext dbcontext;

        public LocationRepository(DoapSoapContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context cant be null.", nameof(context));
            }
            dbcontext = context;
        }

        public List<Library.Location> GetLocations()
        {
            List<Library.Location> result = new List<Library.Location>();

            var entities = dbcontext.Locations;
            foreach(var entity in entities)
            {
                result.Add(Mapper.MapLocation(entity));
            }

            return result;
        }

        /// <summary>
        /// Find a mapped location by ID if found in db, otherwise return null
        /// </summary>
        /// <param name="id">ID of location to be found</param>
        /// <returns>Returns a mapped location if found in db, otherwise return null</returns>
        public Library.Location GetLocationByID(int id)
        {
            return Mapper.MapLocation(dbcontext.Locations.Find(id)) ?? null;
        }
    }
}
