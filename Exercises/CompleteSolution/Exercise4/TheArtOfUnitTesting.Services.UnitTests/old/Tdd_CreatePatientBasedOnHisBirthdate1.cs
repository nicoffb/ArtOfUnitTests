// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tdd_CreatePatientBasedOnHisBirthdate1.cs" company="">
//   
// </copyright>
// <summary>
//   The unit test 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace TheArtOfUnitTesting.Services.UnitTests.Exercise4
{
    /// <summary>
    /// The tdd_ create patient based on his birthdate 1.
    /// </summary>
    [TestFixture]
    public class Tdd_CreatePatientBasedOnHisBirthdate1
    {
        #region Public Methods and Operators

        /// <summary>
        /// The ac c 1_ create_patient_with_wrong_name.
        /// </summary>
        [Test]
        public void ACC1_Create_patient_with_wrong_name()
        {
            // AAA
            var error = Assert.Throws<DomainException>(() => this.CreatePatient(null));
            Assert.IsNotNull(error);
            Assert.IsTrue(error.DomainErrorCode == 1);
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
        private Patient CreatePatient(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                // This is a domain logic error, not technical error.
                throw new DomainException("Domain Error: Patient cannot be created with wrong name.", 1);
            }

            return new Patient() { PatientId = 1, Name = name };
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