﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.Common
{
    public class PaginationEntityDTO
    {
        public string searchTerm {  get; set; }
        public int? pageNumber {  get; set; }
        public int? pageSize { get; set; }
        public string? sortColumn { get; set; }
        public string? sortOrder { get; set; }
    }
}
