import React, { useEffect, useState } from "react";
import PropertyCard from "../components/Property-card.tsx";
import { useNavigate } from "react-router-dom";
import Button from "../components/Button.tsx";

const SwitchRole = () => {
  const navigate = useNavigate();
  const [currentRole, setcurrentRole] = useState("");
  const [loggedIn, setLoggedIn] = useState(false);
  const [role, setRole] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [plan, setPlan] = useState("");
  const [token, setToken] = useState("");

  

  useEffect(() => {
    const loginData = localStorage.getItem("loginData");
    var em, rol, pho, tok, pla;
    if (loginData) {
      em = JSON.parse(loginData)?.email;
      rol = JSON.parse(loginData)?.role;
      pho = JSON.parse(loginData)?.phone;
      tok = JSON.parse(loginData)?.token;
      pla = JSON.parse(loginData)?.plan;
      setcurrentRole(rol);
      setEmail(em)
      setRole(rol)
      setPhone(pho)
      setPlan(pla)
      setToken(tok)
    }
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

  const handleSubmit=() => {
    
   
   
        // var setrole;
        // if(currentRole==="seller")
        // setrole = true;
        // if(currentRole==="buyer")
        //     setrole=false;
        const formData = new FormData();
        formData.append('email', email  );
        formData.append('phone', phone  );
        formData.append('token', token  );
        formData.append('role', role  );
        formData.append('plan', plan  );

        
        
        if (loggedIn && email) {
            
          fetch(`http://localhost:5189/api/Login/SwitchRoles`, {
            method: "POST",
            headers: {
              "Authorization": `Bearer ${JSON.parse(localStorage.getItem("loginData") || '{}')?.token}`,
              "Content-Type": "application/json",
            },
            body: JSON.stringify(formData)
          })
            .then((res) => res.json())
            .then((data) => {
              console.log("data", data);
              localStorage.setItem("loginData", JSON.stringify(data))
              setcurrentRole(data.role);
            })
            .catch((error) => {
              console.error('Fetch error:', error); // Handle any errors that occur during fetch
            });
        
    }
  };

  return (loggedIn && role === "seller" && (
    <div className="flex sm:h-screen justify-start items-center space-x-2 flex-wrap space-y-2">
        {currentRole==="seller"&&(<span>Switch to buyer account?</span>)}
        {currentRole==="buyer"&&(<span>
            Switch to seller account
      </span>)}
      <Button title={`${currentRole==="seller"?"Buyer":"Seller"}`} onClick={handleSubmit} />
      
    </div>
  ));
};

export default SwitchRole;
