import React, { useEffect, useState } from "react";
import PropertyCard from "../components/Property-card.tsx";
import { useNavigate } from "react-router-dom";

const MyProperties = () => {
  const navigate = useNavigate();
  const [properties, setProperties] = useState([]);
  const [loggedIn, setLoggedIn] = useState(false);
  const [role, setRole] = useState("");
  const [email, setEmail] = useState("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const loginData = localStorage.getItem("loginData");
    if (loginData) {
      const parsedData = JSON.parse(loginData);
      const token = parsedData?.token;
      const userEmail = parsedData?.email;
      const userRole = parsedData?.role;

      if (userEmail && userRole) {
        setEmail(userEmail);
        setRole(userRole);
        setLoggedIn(userRole === "seller");
        if (userRole === "buyer") {
          navigate('/view-properties');
        }
      } else {
        navigate('/login');
      }
    } else {
      navigate('/login');
    }
  }, [navigate]);

  useEffect(() => {
    if (loggedIn && role === "seller" && email) {
      fetch('http://localhost:5189/api/Property/GetProperties', {
        method: "GET",
        headers: {
          "Authorization": `Bearer ${JSON.parse(localStorage.getItem("loginData") || '{}')?.token}`,
        },
      })
        .then((res) => res.json())
        .then((data) => {
          console.log("data", data);
          // const filteredProperties = data.filter((property: any) => property.userEmail === email);
          setProperties(data);
        })
        .catch((error) => {
          console.error('Fetch error:', error); // Handle any errors that occur during fetch
        });
    }
  }, [loggedIn, role, email]);

  return (loggedIn && role === "seller" && (
    <div className="flex sm:h-screen justify-start items-center space-x-2 flex-wrap space-y-2 ">
      {properties.map((property: any, i: number) => (
        <div key={i} >
          <PropertyCard propertyData={property} />
        </div>
      ))}
    </div>
  ));
};

export default MyProperties;
