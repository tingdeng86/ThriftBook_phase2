﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.ViewModels
{
    public class InvoiceDetailVM
    {
        [Key]
        [DisplayName("Transaction ID")]
        public int TransactionId { get; set; }
        [DisplayName("Buyer ID")]
        public int BuyerId { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime? DateOfTransaction { get; set; }
        [DisplayName("Book Title")]
        public string Title { get; set; }
        [DisplayName("Book Genre")]
        public string Genre { get; set; }
        [DisplayName("Book ID")]
        public int BookId { get; set; }



    }
}