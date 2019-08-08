using System;
using System.ComponentModel.DataAnnotations;

namespace FCT.WebAPI.Models {
    public class Item {
        [Key]
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemContent { get; set; }
        public int ItemType { get; set; }
    }
}