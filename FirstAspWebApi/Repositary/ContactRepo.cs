﻿using FirstAspWebApi.Data;
using FirstAspWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAspWebApi.Repositary
{
    public class ContactRepo:IContactRepo
    {
        private readonly MyContext _context;

        public ContactRepo(MyContext context)
        {
            _context = context;
        }


        public List<Contact> GetAllData()
        {
            return _context.contacts
                .Where(cont=>cont.IsActive==true)
                .Include(con=>con.ContactDetails)
                .ToList();
        }

        public Contact GetContactById(int id)
        {
            var contact=   _context.contacts.Where(cont1=>cont1.ContactId==id && cont1.IsActive==true)
                .Include(usr => usr.ContactDetails)
                .FirstOrDefault();
            if(contact!=null)
            {
                _context.Entry(contact).State = EntityState.Detached;
            }
            return contact;
        }

        public int Add(Contact Contact)
        {
            _context.contacts.Add(Contact);
            _context.SaveChanges();

            var newContactId = _context.contacts.Where(cont => cont.FirstName == Contact.FirstName)
                .OrderBy(usr => usr.ContactId).LastOrDefault().ContactId;
            return newContactId;
        }

        public Contact  Update(Contact updatedContact)
        {
           // _context.Entry(oldContact).State = EntityState.Detached;//previous contact changing removeing
            _context.Update(updatedContact);//inbuild update
            _context.SaveChanges() ;
            return updatedContact;


            //var contactId = GetContactById(Contact.ContactId);
            //if (contactId != null)
            //{
            //    _context.Entry(contactId).State = EntityState.Detached;
            //    _context.contacts.Update(Contact);
            //    _context.SaveChanges();
            //    return Contact;
            //}
            //return null;
        }

        public void Delete(Contact contact)
        {

            _context.Entry(contact).State = EntityState.Modified;
            contact.IsActive = false;
            foreach (var contactDetails in _context.Details.Where(userr => userr.ContactId == contact.ContactId))
                        _context.Details.Remove(contactDetails);
            _context.SaveChanges();

            //var contactToDelete = GetContactById(id);

            //if (contactToDelete != null)
            //{
            //    contactToDelete.IsActive = false;
            //    _context.SaveChanges();
            //    return true;
            //}
            //return false;
        }
    }
}
