﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octo_movie_cat.Contracts
{
    public class RentalResponseContract
    {
        public bool IsSuccess;
        public Int64? ConfirmationID;
        public string Message;
    }
}
