// import React, { useState } from 'react';
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
// import { faHome, faEye, faPlusCircle, faUser, faSignOutAlt,faArrowUp } from '@fortawesome/free-solid-svg-icons';
// import { Link } from 'react-router-dom';

// const Sidebar = () => {
//     const loginData = localStorage.getItem("loginData");
//     const [loggedIn,setLoggedIn] = useState(false)
//     const [role,setRole] = useState("")
//     if(loginData){
//         setLoggedIn(true)
//         setRole(JSON.parse(loginData)?.role)
//     }
//   return (
//     (loggedIn &&(
//     <nav className="bg-blue-500 h-full w-[7%] p-4 fixed flex flex-col  items-center">
//       <h1 className="text-white text-2xl font-bold mb-6">67</h1>
//       <ul className="flex-1 space-y-4 ">
//         <li>
//           <Link to="/" className="flex items-center text-white  hover:bg-blue-400 p-2 rounded transition-colors">
//             <FontAwesomeIcon icon={faHome} className="h-6 w-6 " />
            
//           </Link>
//         </li>
//         <li>
//             {role==="buyer"&&(
//           <Link to="/view-properties" className="flex items-center text-white  hover:bg-blue-400 p-2 rounded transition-colors">
//             <FontAwesomeIcon icon={faEye} className="h-6 w-6 " />
//           </Link>)}
//             {role==="seller"&&(
//           <Link to="/my-properties" className="flex items-center text-white  hover:bg-blue-400 p-2 rounded transition-colors">
//             <FontAwesomeIcon icon={faEye} className="h-6 w-6 " />
//           </Link>)}

//         </li>
//         <li>
//         {role==="seller"&&(
//           <Link to="/post-property" className="flex items-center text-white  hover:bg-blue-400 p-2 rounded transition-colors">
//             <FontAwesomeIcon icon={faPlusCircle} className="h-6 w-6 " />
//           </Link>)}
//         {role==="buyer"&&(
//           <Link to="/switch-roles" className="flex items-center text-white  hover:bg-blue-400 p-2 rounded transition-colors">
//             <FontAwesomeIcon icon={faPlusCircle} className="h-6 w-6 " />
//           </Link>)}

//         </li>
//         <li>
//           <Link to="/logout" className="flex items-center text-white  hover:bg-blue-400 p-2 rounded transition-colors">
//             <FontAwesomeIcon icon={faSignOutAlt} className="h-6 w-6 " />
//           </Link>
//         </li>
//         <li>
//           <Link to="/upgrade" className="flex items-center text-white  hover:bg-blue-400 p-2 rounded transition-colors">
//             <FontAwesomeIcon icon={faArrowUp} className="h-6 w-6 " />
//           </Link>
//         </li>
//       </ul>
//     </nav>))
//   );
// };

// export default Sidebar;


import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHome, faEye, faPlusCircle, faUser, faSignOutAlt, faArrowUp } from '@fortawesome/free-solid-svg-icons';
import { Link, useNavigate } from 'react-router-dom';

const Sidebar = () => {

  const navigate = useNavigate()

  const [loggedIn, setLoggedIn] = useState(false);
  const [role, setRole] = useState("");

  useEffect(() => {
    const loginData = localStorage.getItem("loginData");
    if (loginData) {
      setLoggedIn(true);
      const parsedData = JSON.parse(loginData);
      setRole(parsedData?.role);
      
    }
  }, []);
  useEffect(() => {
   console.log(role)
  }, []);

  return (
    
      <nav className="bg-blue-500 h-full w-[7%] p-4 fixed flex flex-col items-center">
        <h1 className="text-white text-2xl font-bold mb-6">67</h1>
        <ul className="flex-1 space-y-4">
          <li>
            <Link to="/" className="flex items-center text-white hover:bg-blue-400 p-2 rounded transition-colors">
              <FontAwesomeIcon icon={faHome} className="h-6 w-6" />
            </Link>
          </li>
          <li>
            {role === "buyer" && (
              <Link to="/view-properties" className="flex items-center text-white hover:bg-blue-400 p-2 rounded transition-colors">
                <FontAwesomeIcon icon={faEye} className="h-6 w-6" />
              </Link>
            )}
            {role === "seller" && (
              <Link to="/my-properties" className="flex items-center text-white hover:bg-blue-400 p-2 rounded transition-colors">
                <FontAwesomeIcon icon={faEye} className="h-6 w-6" />
              </Link>
            )}
          </li>
          <li>
            {role === "seller" && (
              <Link to="/post-property" className="flex items-center text-white hover:bg-blue-400 p-2 rounded transition-colors">
                <FontAwesomeIcon icon={faPlusCircle} className="h-6 w-6" />
              </Link>
            )}
            {role === "buyer" && (
              <Link to="/switch-roles" className="flex items-center text-white hover:bg-blue-400 p-2 rounded transition-colors">
                <FontAwesomeIcon icon={faPlusCircle} className="h-6 w-6" />
              </Link>
            )}
          </li>
          <li>
            <button onClick={()=>{localStorage.removeItem("loginData"); navigate('/register'); window.location.reload()}} className="flex items-center text-white hover:bg-blue-400 p-2 rounded transition-colors">
              <FontAwesomeIcon icon={faSignOutAlt} className="h-6 w-6" />
            </button>
          </li>
          <li>
            <Link to="/upgrade" className="flex items-center text-white hover:bg-blue-400 p-2 rounded transition-colors">
              <FontAwesomeIcon icon={faArrowUp} className="h-6 w-6" />
            </Link>
          </li>
        </ul>
      </nav>
    
  );
};

export default Sidebar;
