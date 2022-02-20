﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStoreApplication.Models
{
    /// <summary>
    /// This class holds information on a single mailing Address, including phone number.
    /// </summary>
    public class Address
    {
        //First line of the user's address.
        public string AddressLineOne { get; set; }

        //Second line (optional) of the user's address.
        public string AddressLineTwo { get; set; }

        //City of the user's mailing address.
        public string City { get; set; }

        //State of the user's mailing address.
        public string State { get; set; }

        //Postal code for the user's mailing address.
        public string Zip { get; set; }

        //user's phone number (initially optional)
        [RegularExpression(@"\\(\\d{3}\\)\\d{3}-\\d{4}", ErrorMessage = "Phone number must be in the format (XXX)XXX-XXXX")]
        public string PhoneNumber { get; set; }

        public Address()
        {
        }

        public Address(string addressLineOne, string addressLineTwo, string city, string state, string zip, string phoneNumber)
        {
            AddressLineOne = addressLineOne;
            AddressLineTwo = addressLineTwo;
            City = city;
            State = state;
            Zip = zip;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return AddressLineOne + "\n" + AddressLineTwo + "\n" + City + ", " + State + " " + Zip + "/n" + PhoneNumber;
        }
    }
}
