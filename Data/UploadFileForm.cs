using System;
using Microsoft.AspNetCore.Components.Forms;

namespace blazor_demo.Data
{
    public class UploadFileForm
    {
        public IBrowserFile ExcelFile { get; set; }
    }
}