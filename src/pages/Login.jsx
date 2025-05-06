import { useState } from "react"
import { Link } from "react-router-dom"
import { MdMail, MdLock, MdOutlineVisibility, MdOutlineVisibilityOff } from "react-icons/md"
import Button from "../components/Button"

const Login = () => {
  const [showPassword, setShowPassword] = useState(false)

  const togglePasswordVisibility = () => {
    setShowPassword((prev) => !prev)
  }

  return (
    <div className="min-h-screen flex flex-col md:flex-row bg-gray-50">
      {/* Left column - Blue gradient background */}
      <div className="hidden md:flex md:w-1/2 bg-gradient-to-br from-[#3B6CF7] to-[#4A7CFA] flex-col justify-center items-center text-white p-10 relative overflow-hidden">
        {/* Decorative elements */}
        <div className="absolute top-0 left-0 w-full h-full opacity-10">
          <div className="absolute top-[10%] left-[20%] w-40 h-40 rounded-full bg-white"></div>
          <div className="absolute bottom-[30%] right-[10%] w-60 h-60 rounded-full bg-white"></div>
          <div className="absolute top-[60%] left-[5%] w-20 h-20 rounded-full bg-white"></div>
        </div>

        <div className="w-32 h-32 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center mb-8 shadow-xl relative z-10 transition-all duration-500 hover:scale-105">
          <img
            src="/lovable-uploads/846189b9-c50d-49ad-9a54-6e2370e80b6b.png"
            alt="Login illustration"
            className="w-16 h-16 opacity-0"
          />
        </div>
        <h2 className="text-4xl font-bold mb-4 relative z-10">Welcome Back!</h2>
        <p className="text-xl text-center max-w-md relative z-10">Enter your credentials to access your account</p>
      </div>

      {/* Right column - Login form */}
      <div className="flex flex-1 bg-white md:w-1/2 items-center justify-center shadow-lg">
        <div className="w-full max-w-md px-6 py-10 sm:px-8 md:px-12">
          <div className="mb-12 text-center">
            <h1 className="text-3xl font-bold text-gray-800 mb-2">Log In To Your Account</h1>
            <p className="text-gray-500">Please enter your credentials to continue</p>
          </div>

          {/* Login form */}
          <form className="space-y-6">
            <div className="space-y-2 relative">
            <label className="block text-gray-700 font-medium absolute -top-8 bg-white px-1 text-md transition-all duration-200" htmlFor="email">
                Email
              </label>
              <div className="relative group">
                <MdMail className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400 h-5 w-5 group-hover:text-[#3B6CF7] transition-colors duration-200" />
                <input
                  type="email"
                  id="email"
                  placeholder="Enter your email"
                  className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#3B6CF7] focus:border-[#3B6CF7] bg-white shadow-sm transition-all duration-200 hover:border-gray-400"
                />
              </div>
            </div>

            <div className="space-y-2">
              <div className="flex justify-between items-center">
                <label className="block text-gray-700 font-medium" htmlFor="password">
                  Password
                </label>
                <Link
                  to="/forgot-password"
                  className="text-[#3B6CF7] hover:text-[#2A5CE4] text-sm font-medium transition-colors duration-200"
                >
                  Forgot Password?
                </Link>
              </div>
              <div className="relative group">
                <MdLock className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400 h-5 w-5 group-hover:text-[#3B6CF7] transition-colors duration-200" />
                <input
                  type={showPassword ? "text" : "password"}
                  id="password"
                  placeholder="Enter your password"
                  className="w-full pl-10 pr-10 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#3B6CF7] focus:border-[#3B6CF7] bg-white shadow-sm transition-all duration-200 hover:border-gray-400"
                />
                <button
                  type="button"
                  onClick={togglePasswordVisibility}
                  className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600 transition-colors duration-200"
                >
                  {showPassword ? (
                    <MdOutlineVisibilityOff className="h-5 w-5" />
                  ) : (
                    <MdOutlineVisibility className="h-5 w-5" />
                  )}
                </button>
              </div>
            </div>

            <Button
              type="submit"
              className="w-full py-3.5 bg-[#3B6CF7] hover:bg-[#2A5CE4] text-white font-medium rounded-lg text-center transition-all duration-300 shadow-md hover:shadow-lg mt-4 transform hover:-translate-y-0.5"
            >
              Sign In
            </Button>
          </form>

          <div className="mt-10 text-center">
            <p className="text-gray-600">
              Don't have an account?{" "}
              <Link
                to="/signup"
                className="text-[#3B6CF7] hover:text-[#2A5CE4] font-medium transition-colors duration-200"
              >
                Sign up
              </Link>
            </p>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Login
