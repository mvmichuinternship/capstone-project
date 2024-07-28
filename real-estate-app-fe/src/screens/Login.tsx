import React, { useState } from "react";
import Container from "../components/Container.tsx";
import Card from "../components/Card.tsx";
import Button from "../components/Button.tsx";

const Register = () => {
  const [userData, setUserData] = useState({
    UserEmail: "",
    Password: "",
  });

  const [emailError, setEmailError] = useState("");
  const [nameError, setNameError] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [phoneError, setPhoneError] = useState("");
  

  const validateEmail = (email) => {
    if (
      String(email)
        .toLowerCase()
        .match(
          /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        )
    ) {
      setEmailError("");
    } else {
      setEmailError("Enter valid Email");
    }
  };
 
  const validatePassword = (email) => {
    if (
      String(email)
        .match(/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{6,}$/)
    ) {
      setPasswordError("");
    } else {
      setPasswordError("Password is weak");
    }
  };

  const validatePhone = (email) => {
    if (
      String(email).toLowerCase().length >= 1 &&
      String(email).toLowerCase().length <= 10
    ) {
      setPhoneError("");
    } else {
      setPhoneError("Enter valid Phone Number");
    }
  };
  

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setUserData((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append("UserEmail", userData.UserEmail);
    formData.append("Password", userData.Password);

    // try {
    //   const response = await fetch("", {
    //     method: "POST",
    //     body: formData,
    //   });

    //   if (response.ok) {
    //     console.log("Property and image uploaded successfully");
    //   } else {
    //     console.log("Upload failed");
    //   }
    // } catch (error) {
    //   console.error("Error uploading property:", error);
    // }
    console.log(userData)
  };

  return (
    <Container>
      <Card>
        {/* <form action="" className="w-full space-y-2"> */}
        <span className="text-2xl">Register</span>
        <input
          className="border border-neutral-300 rounded-md w-full px-4 py-1 focus:outline-blue-400"
          name="UserEmail"
          id="UserEmail"
          type="email"
          placeholder="Enter Email"
          onChange={(e: any) => {
            validateEmail(e.target.value);
            handleInputChange(e);
          }}
          value={userData.UserEmail}
        />
        <span className="text-xs self-start w-full text-red-500">{emailError}</span>
       
        
        <input
          className="border border-neutral-300 rounded-md w-full px-4 py-1 focus:outline-blue-400"
          name="Password"
          id="Password"
          type="password"
          placeholder="Enter Password"
          onChange={(e: any) => {
            validatePassword(e.target.value);
            handleInputChange(e);
          }}
          value={userData.Password}
        />
        <span className="text-xs self-start w-full text-red-500">{passwordError}</span>
        
       
        <Button onClick={handleSubmit} title="Register" />

        <span className="py-4">
          Not yet registered?
          <a
            href="/signup"
            className="ml-2 font-medium text-blue-400 no-underline "
          >
            Sign Up
          </a>
        </span>
        {/* </form> */}
      </Card>
    </Container>
  );
};

export default Register;
