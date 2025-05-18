import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import {
  MdMail,
  MdLock,
  MdPerson,
  MdOutlineVisibility,
  MdOutlineVisibilityOff,
} from "react-icons/md";
import Button from "../components/Button";
import axios from "axios";

const Login = () => {
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState(null);
  const [validationErrors, setValidationErrors] = useState({
    email: "",
    password: "",
  });
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const validateEmail = (email) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  };

  const togglePasswordVisibility = () => {
    setShowPassword((prev) => !prev);
  };

  const handleChange = (e) => {
    const { id, value } = e.target;
    setFormData((prev) => ({ ...prev, [id]: value }));
    setError(""); // Clear error when user types
    
    // Clear validation error for the field being changed
    setValidationErrors(prev => ({
      ...prev,
      [id]: ""
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);

    // Reset validation errors
    const newValidationErrors = {
      email: "",
      password: "",
    };

    // Validate email
    if (!formData.email.trim()) {
      newValidationErrors.email = "Email is required";
    } else if (!validateEmail(formData.email)) {
      newValidationErrors.email = "Please enter a valid email address";
    }

    // Validate password
    if (!formData.password) {
      newValidationErrors.password = "Password is required";
    }

    // Update validation errors
    setValidationErrors(newValidationErrors);

    // Check if there are any validation errors
    const hasErrors = Object.values(newValidationErrors).some(error => error !== "");
    if (hasErrors) {
      return; // Don't proceed with login if there are validation errors
    }

    try {
      const response = await axios.post("https://localhost:7098/api/auth/login", {
        email: formData.email,
        password: formData.password,
      });

      const { token, refreshToken, userId, role } = response.data.data;
      localStorage.setItem("token", token);
      localStorage.setItem("refreshToken", refreshToken);
      localStorage.setItem("userId", userId);
      localStorage.setItem("role", role);
      navigate("/Home");
    } catch (err) {
      console.error("Login error:", err);
      setError(err.response?.data?.message || "Login failed");
    }
  };

  return (
    <div className="flex flex-col md:flex-row bg-gradient-to-br from-blue-50 to-indigo-50 min-h-screen">
      {/* Left column - Enhanced background */}
      <div className="hidden min-h-screen md:flex md:w-2/3 bg-gradient-to-br from-[#3B6CF7] to-[#4A7CFA] flex-col justify-center items-center text-white p-10 relative overflow-hidden">
        {/* Animated decorative elements */}
        <div className="absolute top-0 left-0 w-full h-full opacity-10">
          <div className="absolute top-[10%] left-[20%] w-40 h-40 rounded-full bg-white animate-float"></div>
          <div className="absolute bottom-[30%] right-[10%] w-60 h-60 rounded-full bg-white animate-float-delayed"></div>
          <div className="absolute top-[60%] left-[5%] w-20 h-20 rounded-full bg-white animate-pulse"></div>
        </div>

        <div className="w-32 h-32 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center mb-8 shadow-xl relative z-10 transition-all duration-500 hover:scale-105 hover:shadow-2xl">
          <MdPerson className="text-white h-20 w-20 opacity-90 animate-bounce-in" />
        </div>
        <h2 className="text-4xl font-bold mb-4 relative z-10 animate-slide-in">
          Welcome Back!
        </h2>
        <p className="text-xl text-center max-w-md relative z-10 animate-slide-in-delayed">
          Enter your credentials to access your account
        </p>
      </div>

      {/* Right column - Enhanced form */}
      <div className="flex flex-grow min-h-screen bg-white/90 backdrop-blur-lg md:w-1/2 items-center justify-center shadow-xl rounded-l-2xl overflow-hidden">
        <div className="w-full max-w-md px-6 py-10 sm:px-8 md:px-12">
          <div className="mb-12 text-center animate-fade-in">
            <h1 className="text-3xl font-bold text-gray-800 mb-2 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] bg-clip-text text-transparent">
              Log In To Your Account
            </h1>
            <p className="text-gray-500 mt-1">
              Please enter your credentials to continue
            </p>
          </div>

          {/* Error message display */}
          {error && (
            <div className="mb-4 p-3 bg-red-100 text-red-700 rounded-lg animate-fade-in">
              {error}
            </div>
          )}

          {/* Enhanced form */}
          <form className="space-y-6 animate-fade-in-up" onSubmit={handleSubmit}>
            <div className="group relative">
              <input
                type="email"
                id="email"
                value={formData.email}
                onChange={handleChange}
                className={`w-full px-4 py-3 pl-12 border-2 ${
                  validationErrors.email ? "border-red-500" : "border-gray-200"
                } rounded-xl focus:border-[#3B6CF7] focus:ring-0 peer transition-all duration-300 hover:border-gray-300 placeholder-transparent`}
                placeholder=" "
              />
              <label
                htmlFor="email"
                className="absolute left-12 -top-2.5 px-1 bg-white text-gray-500 text-sm transition-all duration-300 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-placeholder-shown:top-3.5 peer-focus:-top-2.5 peer-focus:text-sm peer-focus:text-[#3B6CF7]"
              >
                Email Address
              </label>
              <MdMail className="absolute left-4 top-1/2 -translate-y-1/2 text-gray-400 h-6 w-6 transition-colors duration-300 peer-focus:text-[#3B6CF7]" />
              {validationErrors.email && (
                <p className="text-red-500 text-sm mt-1">{validationErrors.email}</p>
              )}
            </div>

            {/* Password input with enhanced interactions */}
            <div className="group relative">
              <input
                type={showPassword ? "text" : "password"}
                id="password"
                value={formData.password}
                onChange={handleChange}
                className={`w-full px-4 py-3 pl-12 pr-12 border-2 ${
                  validationErrors.password ? "border-red-500" : "border-gray-200"
                } rounded-xl focus:border-[#3B6CF7] focus:ring-0 peer transition-all duration-300 hover:border-gray-300 placeholder-transparent`}
                placeholder=" "
              />
              <label
                htmlFor="password"
                className="absolute left-12 -top-2.5 px-1 bg-white text-gray-500 text-sm transition-all duration-300 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-placeholder-shown:top-3.5 peer-focus:-top-2.5 peer-focus:text-sm peer-focus:text-[#3B6CF7]"
              >
                Password
              </label>
              <MdLock className="absolute left-4 top-1/2 -translate-y-1/2 text-gray-400 h-6 w-6 transition-colors duration-300 peer-focus:text-[#3B6CF7]" />
              <button
                type="button"
                onClick={togglePasswordVisibility}
                className="absolute right-4 top-1/2 -translate-y-1/2 text-gray-400 hover:text-[#3B6CF7] transition-colors duration-300"
              >
                {showPassword ? (
                  <MdOutlineVisibilityOff className="h-6 w-6" />
                ) : (
                  <MdOutlineVisibility className="h-6 w-6" />
                )}
              </button>
              {validationErrors.password && (
                <p className="text-red-500 text-sm mt-1">{validationErrors.password}</p>
              )}
            </div>

            <Button
              type="submit"
              className="w-full py-3.5 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] text-white font-semibold rounded-xl transition-all duration-300 hover:shadow-lg hover:scale-[1.02] active:scale-95 shadow-md hover:shadow-[#3B6CF7]/30"
            >
              Sign In
            </Button>
          </form>

          {/* Enhanced footer */}
          <div className="mt-10 text-center animate-fade-in">
            <p className="text-gray-600">
              Don't have an account?{" "}
              <Link
                to="/signup"
                className="font-medium bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] bg-clip-text text-transparent hover:underline"
              >
                Sign up now
              </Link>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;