﻿using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPatientService
    {
        Task<ResultDto> AddPatientAsync(PatientDto patient);
        Task<ResultDto<List<PatientsListDto>?>> GetPatientListAsync();
    }
}
