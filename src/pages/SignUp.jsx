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
import authService from "../services/authService";

const SignUp = () => {
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const [formData, setFormData] = useState({
    email: "",
    username: "",
    password: "",
    confirmPassword: "",
  });

  const handleChange = (e) => {
    const { id, value } = e.target;
    setFormData((prev) => ({ ...prev, [id]: value }));
    setError(""); // Clear error when user types
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setLoading(true);

    // Validate passwords match
    if (formData.password !== formData.confirmPassword) {
      setError("Passwords do not match");
      setLoading(false);
      return;
    }

    try {
      const response = await authService.register({
        email: formData.email,
        username: formData.username,
        password: formData.password,
      });

      // If registration is successful, redirect to login
      navigate("/login", {
        state: { message: "Registration successful! Please login." },
      });
    } catch (error) {
      setError(error.message || "Registration failed. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  const togglePasswordVisibility = () => {
    setShowPassword((prev) => !prev);
  };

  const toggleConfirmPasswordVisibility = () => {
    setShowConfirmPassword((prev) => !prev);
  };

  return (
    <div className="min-h-[96.5vh] flex flex-col md:flex-row bg-gradient-to-br from-blue-50 to-indigo-50">
      {/* Left column - Enhanced background */}
      <div className="hidden min-h-[96.5vh] md:flex md:w-2/3 bg-gradient-to-br from-[#3B6CF7] to-[#4A7CFA] flex-col justify-center items-center text-white p-10 relative overflow-hidden">
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
          Create Account
        </h2>
        <p className="text-xl text-center max-w-md relative z-10 animate-slide-in-delayed">
          Join our community and start your journey
        </p>
      </div>

      {/* Right column - Enhanced form */}
      <div className="flex flex-grow min-h-[96.5vh] bg-white/90 backdrop-blur-lg md:w-1/2 items-center justify-center shadow-xl rounded-l-2xl overflow-hidden">
        <div className="w-full max-w-md px-6 py-10 sm:px-8 md:px-12">
          <div className="mb-12 text-center animate-fade-in">
            <h1 className="text-3xl font-bold text-gray-800 mb-2 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] bg-clip-text text-transparent">
              Sign Up for BookWorm
            </h1>
            <p className="text-gray-500 mt-1">
              Create your account to get started
            </p>
          </div>

          {/* Enhanced sign up form */}
          <form
            className="space-y-6 animate-fade-in-up"
            onSubmit={handleSubmit}
          >
            {error && (
              <div className="p-3 bg-red-50 border border-red-200 text-red-600 rounded-lg text-sm">
                {error}
              </div>
            )}

            {/* Email Input */}
            <div className="group relative">
              <input
                type="email"
                id="email"
                className="w-full px-4 py-3 pl-12 border-2 border-gray-200 rounded-xl focus:border-[#3B6CF7] focus:ring-0 peer transition-all duration-300 hover:border-gray-300 placeholder-transparent"
                placeholder=" "
                value={formData.email}
                onChange={handleChange}
                required
              />
              <label
                htmlFor="email"
                className="absolute left-12 -top-2.5 px-1 bg-white text-gray-500 text-sm transition-all duration-300 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-placeholder-shown:top-3.5 peer-focus:-top-2.5 peer-focus:text-sm peer-focus:text-[#3B6CF7]"
              >
                Email Address
              </label>
              <MdMail className="absolute left-4 top-1/2 -translate-y-1/2 text-gray-400 h-6 w-6 transition-colors duration-300 peer-focus:text-[#3B6CF7]" />
            </div>

            {/* Username Input */}
            <div className="group relative">
              <input
                type="text"
                id="username"
                className="w-full px-4 py-3 pl-12 border-2 border-gray-200 rounded-xl focus:border-[#3B6CF7] focus:ring-0 peer transition-all duration-300 hover:border-gray-300 placeholder-transparent"
                placeholder=" "
                value={formData.username}
                onChange={handleChange}
                required
              />
              <label
                htmlFor="username"
                className="absolute left-12 -top-2.5 px-1 bg-white text-gray-500 text-sm transition-all duration-300 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-placeholder-shown:top-3.5 peer-focus:-top-2.5 peer-focus:text-sm peer-focus:text-[#3B6CF7]"
              >
                Username
              </label>
              <MdPerson className="absolute left-4 top-1/2 -translate-y-1/2 text-gray-400 h-6 w-6 transition-colors duration-300 peer-focus:text-[#3B6CF7]" />
            </div>

            {/* Password Input */}
            <div className="group relative">
              <input
                type={showPassword ? "text" : "password"}
                id="password"
                className="w-full px-4 py-3 pl-12 pr-12 border-2 border-gray-200 rounded-xl focus:border-[#3B6CF7] focus:ring-0 peer transition-all duration-300 hover:border-gray-300 placeholder-transparent"
                placeholder=" "
                value={formData.password}
                onChange={handleChange}
                required
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
            </div>

            {/* Confirm Password Input */}
            <div className="group relative">
              <input
                type={showConfirmPassword ? "text" : "password"}
                id="confirmPassword"
                className="w-full px-4 py-3 pl-12 pr-12 border-2 border-gray-200 rounded-xl focus:border-[#3B6CF7] focus:ring-0 peer transition-all duration-300 hover:border-gray-300 placeholder-transparent"
                placeholder=" "
                value={formData.confirmPassword}
                onChange={handleChange}
                required
              />
              <label
                htmlFor="confirmPassword"
                className="absolute left-12 -top-2.5 px-1 bg-white text-gray-500 text-sm transition-all duration-300 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-placeholder-shown:top-3.5 peer-focus:-top-2.5 peer-focus:text-sm peer-focus:text-[#3B6CF7]"
              >
                Confirm Password
              </label>
              <MdLock className="absolute left-4 top-1/2 -translate-y-1/2 text-gray-400 h-6 w-6 transition-colors duration-300 peer-focus:text-[#3B6CF7]" />
              <button
                type="button"
                onClick={toggleConfirmPasswordVisibility}
                className="absolute right-4 top-1/2 -translate-y-1/2 text-gray-400 hover:text-[#3B6CF7] transition-colors duration-300"
              >
                {showConfirmPassword ? (
                  <MdOutlineVisibilityOff className="h-6 w-6" />
                ) : (
                  <MdOutlineVisibility className="h-6 w-6" />
                )}
              </button>
            </div>

            <Button
              type="submit"
              disabled={loading}
              className="w-full py-3.5 bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] text-white font-semibold rounded-xl transition-all duration-300 hover:shadow-lg hover:scale-[1.02] active:scale-95 shadow-md hover:shadow-[#3B6CF7]/30 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {loading ? "Creating Account..." : "Create Account"}
            </Button>
          </form>

          {/* Enhanced footer */}
          <div className="mt-10 text-center animate-fade-in">
            <p className="text-gray-600">
              Already have an account?{" "}
              <Link
                to="/login"
                className="font-medium bg-gradient-to-r from-[#3B6CF7] to-[#4A7CFA] bg-clip-text text-transparent hover:underline"
              >
                Sign in
              </Link>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default SignUp;
