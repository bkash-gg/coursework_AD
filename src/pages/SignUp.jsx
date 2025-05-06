"use client"

import { useState } from "react"
import { Link } from "react-router-dom"
import { MdMail, MdLock, MdPerson, MdOutlineVisibility, MdOutlineVisibilityOff } from "react-icons/md"
import Button from "../components/Button"

const SignUp = () => {
  const [showPassword, setShowPassword] = useState(false)
  const [showConfirmPassword, setShowConfirmPassword] = useState(false)

  const togglePasswordVisibility = () => {
    setShowPassword((prev) => !prev)
  }

  const toggleConfirmPasswordVisibility = () => {
    setShowConfirmPassword((prev) => !prev)
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
          {/* You can add an icon or image here if needed */}
          <MdPerson className="text-white h-16 w-16 opacity-70" />
        </div>
        <h2 className="text-4xl font-bold mb-4 relative z-10">Create Account</h2>
        <p className="text-xl text-center max-w-md relative z-10">Join our community and start your journey</p>
      </div>

      {/* Right column - Sign up form */}
      <div className="flex flex-1 bg-white md:w-1/2 items-center justify-center shadow-lg">
        <div className="w-full max-w-md px-6 py-10 sm:px-8 md:px-12">
          <div className="mb-12 text-center">
            <h1 className="text-3xl font-bold text-gray-800 mb-2">Sign Up for BookWorm</h1>
            <p className="text-gray-500">Create your account to get started</p>
          </div>
          {/* Sign up form */}
          <form className="space-y-6">
            <div className="space-y-2 relative">
              <label className="block text-gray-700 font-medium absolute -top-7 bg-white px-1  text-md transition-all duration-200" htmlFor="email">
                Email
              </label>
              <div className="relative group">
                <MdMail className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400 h-5 w-5 group-hover:text-[#3B6CF7] transition-colors duration-200" />
                <input
                  type="email"
                  id="email"
                  placeholder="Enter your email"
                  className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#3B6CF7] focus:border-[#3B6CF7] bg-white shadow-sm transition-all duration-200 hover:border-gray-400"
                  required
                />
              </div>
            </div>

            <div className="space-y-2 relative">
              <label className="block text-gray-700 font-medium absolute -top-7 bg-white px-1 text-md transition-all duration-200" htmlFor="username">
                Username
              </label>
              <div className="relative group">
                <MdPerson className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400 h-5 w-5 group-hover:text-[#3B6CF7] transition-colors duration-200" />
                <input
                  type="text"
                  id="username"
                  placeholder="Enter your username"
                  className="w-full px-4 pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#3B6CF7] focus:border-[#3B6CF7] bg-white shadow-sm transition-all duration-200 hover:border-gray-400"
                  required
                />
              </div>
            </div>

            <div className="space-y-2 relative">
              <label className="block text-gray-700 font-medium absolute -top-7" htmlFor="password">
                Password
              </label>
              <div className="relative group">
                <MdLock className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400 h-5 w-5 group-hover:text-[#3B6CF7] transition-colors duration-200" />
                <input
                  type={showPassword ? "text" : "password"}
                  id="password"
                  placeholder="Enter your password"
                  className="w-full pl-10 pr-10 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#3B6CF7] focus:border-[#3B6CF7] bg-white shadow-sm transition-all duration-200 hover:border-gray-400"
                  required
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

            <div className="space-y-2 relative">
              <label className="block text-gray-700 font-medium absolute -top-7" htmlFor="confirm-password">
                Confirm Password
              </label>
              <div className="relative group">
                <MdLock className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400 h-5 w-5 group-hover:text-[#3B6CF7] transition-colors duration-200" />
                <input
                  type={showConfirmPassword ? "text" : "password"}
                  id="confirm-password"
                  placeholder="Confirm your password"
                  className="w-full pl-10 pr-10 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-[#3B6CF7] focus:border-[#3B6CF7] bg-white shadow-sm transition-all duration-200 hover:border-gray-400"
                  required
                />
                <button
                  type="button"
                  onClick={toggleConfirmPasswordVisibility}
                  className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600 transition-colors duration-200"
                >
                  {showConfirmPassword ? (
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
              Create Account
            </Button>
          </form>

          <div className="mt-10 text-center">
            <p className="text-gray-600">
              Already have an account?{" "}
              <Link
                to="/login"
                className="text-[#3B6CF7] hover:text-[#2A5CE4] font-medium transition-colors duration-200"
              >
                Sign in
              </Link>
            </p>
          </div>
        </div>
      </div>
    </div>
  )
}

export default SignUp

