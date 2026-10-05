// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tdd_CreatePatientBasedOnHisBirthdate2.cs" company="">
//   
// </copyright>
// <summary>
//   The unit test 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace TheArtOfUnitTesting.Services.UnitTests.Exercise4
{
    /// <summary>
    /// The tdd_ create patient based on his birthdate 2.
    /// </summary>
    [TestFixture]
    public class Tdd_CreatePatientBasedOnHisBirthdate2
    {
        #region Public Methods and Operators

        /// <summary>
        /// The ac c 1_ create_patient_with_wrong_name.
        /// </summary>
        [Test]
        public void ACC1_Create_patient_with_wrong_name()
        {
            // AAA
            var error = Assert.Throws<DomainException>(() => this.CreatePatient(null, DateTime.Now.AddDays(-3)));
            Assert.IsTrue(error.DomainErrorCode == 1);
        }

        /// <summary>
        /// The ac c 2_ create_patient_with_wrong_birthdate.
        /// </summary>
        [Test]
        public void ACC2_Create_patient_with_wrong_birthdate()
        {
            // AAA
            var error = Assert.Throws<DomainException>(() => this.CreatePatient("Benjami", DateTime.Now.AddDays(3)));
            Assert.IsTrue(error.DomainErrorCode == 2);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create patient.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="birthdate">
        /// The birthdate.
        /// </param>
        /// <returns>
        /// The <see cref="Patient"/>.
        /// </returns>
        /// <exception cref="DomainException">
        /// </exception>
        private Patient CreatePatient(string name, DateTime birthdate)
        {
            if (string.IsNullOrEmpty(name))
            {
                // This is a domain logic error, not technical error.
                throw new DomainException("Domain Error: Patient cannot be created with wrong name.", 1);
            }
            
            if (birthdate.Date >= DateTime.Today)
            {
                // This is a domain logic error, not technical error.
                throw new DomainException("Domain Error: Patient birthdate has to be less than today", 2);
            }

            return new Patient() { PatientId = 1, Name = name, BirthDate = birthdate };
        }

        #endregion

        /// <summary>
        ///     The domain exception.
        /// </summary>
        public class DomainException : Exception
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="DomainException"/> class.
            /// </summary>
            /// <param name="message">
            /// The message.
            /// </param>
            /// <param name="code">
            /// The code.
            /// </param>
            public DomainException(string message, int code)
                : base(message)
            {
                this.DomainErrorCode = code;
            }

            #endregion

            #region Public Properties

            /// <summary>
            ///     Gets or sets the domain error code.
            /// </summary>
            public int DomainErrorCode { get; set; }

            #endregion
        }
    }
}